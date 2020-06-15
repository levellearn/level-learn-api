using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;
using LevelLearn.Resource.Usuarios;
using LevelLearn.Service.Interfaces.Comum;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.Service.Services.Comum;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        #region Ctor

        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IArquivoService _arquivoService;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        private readonly ISharedResource _sharedResource;
        private readonly UsuarioResource _usuarioResource;
        private readonly PessoaResource _pessoaResource;

        private readonly ILogger<UsuarioService> _log;

        private Usuario _usuarioLogado = null;

        public UsuarioService(
            IUnitOfWork uow,
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            ITokenService tokenService,
            IEmailService emailService,
            IArquivoService arquivoService,
            ISharedResource sharedResource,
            ILogger<UsuarioService> logger)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _arquivoService = arquivoService;

            _sharedResource = sharedResource;
            _usuarioResource = UsuarioResource.ObterInstancia();
            _pessoaResource = PessoaResource.ObterInstancia();

            _log = logger;
        }

        #endregion

        public async Task<ResultadoService<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);
            var celular = new Celular(usuarioVM.Celular);
            var cpf = new CPF(usuarioVM.Cpf);

            // Validações BD
            ResultadoService<UsuarioVM> respostaValidacao = await ValidarCriarUsuarioBD(email.Endereco, cpf.Numero);
            if (respostaValidacao.Falhou) return respostaValidacao;

            // Criação e Validação PROFESSOR
            var professor = new Professor(usuarioVM.Nome, email, cpf, celular, usuarioVM.Genero, usuarioVM.DataNascimento);

            if (!professor.EstaValido())
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(professor.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Criação e Validação USER
            var usuario = new Usuario(professor.Nome, usuarioVM.NickName, email.Endereco, celular.Numero, professor.Id);
            usuario.AtribuirSenha(usuarioVM.Senha, usuarioVM.ConfirmacaoSenha);

            if (!usuario.EstaValido())
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Criando PROFESSOR BD
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CompleteAsync()) return ResultadoServiceFactory<UsuarioVM>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando USUÁRIO Identity
            var role = ApplicationRoles.PROFESSOR;
            ResultadoService<UsuarioVM> resultadoIdentity = await CriarUsuarioIdentity(usuario, role, professor);
            if (resultadoIdentity.Falhou) return resultadoIdentity;

            // EMAIL de confirmação
            _ = EnviarEmail(professor, usuario, role);

            return ResultadoServiceFactory<UsuarioVM>.NoContent(_sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> LogarUsuario(LoginUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);

            // Validações
            switch (usuarioVM.TipoAutenticacao)
            {
                case TipoAutenticacao.Senha:
                    {
                        var respostaValidacao = await ValidarCredenciaisUsuario(email, usuarioVM.Senha);
                        if (respostaValidacao.Falhou) return respostaValidacao;
                        break;
                    }
                case TipoAutenticacao.Refresh_Token:
                    {
                        var respostaValidacao = await ValidarRefreshToken(email, usuarioVM.RefreshToken);
                        if (respostaValidacao.Falhou) return respostaValidacao;
                        break;
                    }
                default:
                    return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Tipo de autenticação inválida");
            }

            // Gerar VM e JWToken  
            _usuarioLogado ??= await _userManager.FindByNameAsync(email.Endereco);
            var roles = await _userManager.GetRolesAsync(_usuarioLogado);

            var responseVM = new UsuarioTokenVM()
            {
                Id = _usuarioLogado.Id,
                Nome = _usuarioLogado.Nome,
                NickName = _usuarioLogado.NickName,
                ImagemUrl = _usuarioLogado.ImagemUrl,
                Token = await _tokenService.GerarJWT(_usuarioLogado, roles)
            };

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioLoginSucesso);
        }

        public async Task<ResultadoService<UsuarioVM>> Logout(string jwtId)
        {
            await _signInManager.SignOutAsync();
            await _tokenService.InvalidarTokenERefreshTokenCache(jwtId);

            return ResultadoServiceFactory<UsuarioVM>.NoContent(_usuarioResource.UsuarioLogoutSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(confirmationToken))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_sharedResource.DadosInvalidos);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return ResultadoServiceFactory<UsuarioTokenVM>.NotFound(_sharedResource.NaoEncontrado);

            var tokenDecoded = confirmationToken.DecodeBase64ToText();

            // Verificando confirmação EMAIL Identity
            var identityResult = await _userManager.ConfirmEmailAsync(user, tokenDecoded);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioEmailConfirmarFalha);

            var roles = await _userManager.GetRolesAsync(user);

            var responseVM = new UsuarioTokenVM()
            {
                Id = user.Id,
                Nome = user.Nome,
                NickName = user.NickName,
                ImagemUrl = user.ImagemUrl,
                Token = await _tokenService.GerarJWT(user, roles)
            };

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioEmailConfirmarSucesso);
        }

        public async Task<ResultadoService<UsuarioVM>> AlterarFotoPerfil(string userId, IFormFile arquivo)
        {
            const int TAMANHO_MAXIMO_BYTES = 5_000_000;
            DiretoriosFirebase diretorio = DiretoriosFirebase.ImagensPerfilUsuario;
            var mimeTypesAceitos = new string[] { "image/jpeg", "image/png", "image/gif" };

            Usuario usuario = await _userManager.FindByIdAsync(userId);

            // Validações BD
            if (usuario == null) return ResultadoServiceFactory<UsuarioVM>.NotFound(_sharedResource.NaoEncontrado);

            // Validações Arquivo
            if (arquivo == null || arquivo.Length <= 0 || arquivo.Length > TAMANHO_MAXIMO_BYTES)
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(_sharedResource.DadosInvalidos);

            if (!mimeTypesAceitos.Any(m => m == arquivo.ContentType))
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(_sharedResource.DadosInvalidos);

            Stream imagemRedimensionada = RedimensionarImagem(arquivo);

            // Upload Firebase Storage
            string nomeArquivo = usuario.GerarNomeFotoPerfil();

            string imagemUrl = await _arquivoService.SalvarArquivo(imagemRedimensionada, diretorio, nomeArquivo);

            string nomeImagemAnterior = usuario.AlterarFotoPerfil(imagemUrl, nomeArquivo);

            _ = _arquivoService.DeletarArquivo(diretorio, nomeImagemAnterior);

            // Atualizando USUÁRIO BD
            IdentityResult identityResult = await _userManager.UpdateAsync(usuario);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);

            var responseVM = new UsuarioVM()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                NickName = usuario.NickName,
                ImagemUrl = usuario.ImagemUrl
            };

            return ResultadoServiceFactory<UsuarioVM>.Ok(responseVM, _sharedResource.AtualizadoSucesso);
        }


        #region Privates Methods

        private async Task<ResultadoService<UsuarioVM>> ValidarCriarUsuarioBD(string email, string cpf)
        {
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == email))
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(_usuarioResource.UsuarioEmailJaExiste);
            // TODO: Revisar
            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == cpf))
                return ResultadoServiceFactory<UsuarioVM>.BadRequest(_pessoaResource.PessoaCPFJaExiste);            

            return ResultadoServiceFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResultadoService<UsuarioTokenVM>> ValidarCredenciaisUsuario(Email email, string senha)
        {
            if (!email.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(email.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            // Validações Senha
            if (string.IsNullOrWhiteSpace(senha))
            {
                var dadoInvalido = new DadoInvalido("Senha", _usuarioResource.UsuarioSenhaObrigatoria);
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(dadoInvalido, _sharedResource.DadosInvalidos);
            }

            var senhaTamanhoMin = RegraUsuario.SENHA_TAMANHO_MIN;
            var senhaTamanhoMax = RegraUsuario.SENHA_TAMANHO_MAX;

            if (senha.Length < senhaTamanhoMin || senha.Length > senhaTamanhoMax)
            {
                var dadoInvalido = new DadoInvalido("Senha", _usuarioResource.UsuarioSenhaTamanho(senhaTamanhoMin, senhaTamanhoMax));
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(dadoInvalido, _sharedResource.DadosInvalidos);
            }

            // Sign in Identity            
            _usuarioLogado = await _userManager.FindByNameAsync(email.Endereco);
            if (_usuarioLogado == null)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            if (!_usuarioLogado.EmailConfirmed && await _userManager.CheckPasswordAsync(_usuarioLogado, senha))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioEmailNaoConfirmado);

            var result = await _signInManager.CheckPasswordSignInAsync(_usuarioLogado, senha, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioContaBloqueada);

            if (!result.Succeeded)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            return ResultadoServiceFactory<UsuarioTokenVM>.NoContent();
        }

        private async Task<ResultadoService<UsuarioTokenVM>> ValidarRefreshToken(Email email, string refreshToken)
        {
            // Validações
            if (!email.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>
                    .BadRequest(email.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (string.IsNullOrWhiteSpace(refreshToken))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token precisa estar preenchida");

            // Verifica REFRESH TOKEN no BD cache 
            string tokenArmazenadoCache = await _tokenService.ObterRefreshTokenCache(refreshToken);

            if (string.IsNullOrWhiteSpace(tokenArmazenadoCache))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token expirado ou não existente");

            var refreshTokenData = JsonConvert.DeserializeObject<RefreshTokenData>(tokenArmazenadoCache);

            var credenciaisValidas = (email.Endereco == refreshTokenData.Email
                                        && refreshToken == refreshTokenData.RefreshToken);

            if (!credenciaisValidas)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token inválido");

            // Remove o REFRESH TOKEN já que um novo será gerado
            await _tokenService.InvalidarRefreshTokenCache(refreshToken);

            return ResultadoServiceFactory<UsuarioTokenVM>.NoContent();
        }

        private async Task<ResultadoService<UsuarioVM>> CriarUsuarioIdentity(Usuario user, string role, Pessoa pessoa)
        {
            try
            {
                var identityResult = await _userManager.CreateAsync(user, user.Senha);
                if (!identityResult.Succeeded)
                {
                    await RemoverPessoa(pessoa);
                    return ResultadoServiceFactory<UsuarioVM>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
                }

                await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return ResultadoServiceFactory<UsuarioVM>.NoContent();
            }
            catch (Exception)
            {
                await RemoverUsuario(user, role);
                await RemoverPessoa(pessoa);
                return ResultadoServiceFactory<UsuarioVM>.InternalServerError(_sharedResource.ErroInternoServidor);
            }
        }

        private async Task<ResultadoService<UsuarioVM>> EnviarEmail(Professor professor, Usuario user, string role)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var tokenEncoded = confirmationToken.EncodeTextToBase64();

            try
            {
                await _emailService.EnviarEmailCadastroProfessor(user.Email, professor.Nome, user.Id, tokenEncoded);
            }
            catch (Exception)
            {
                await RemoverUsuario(user, role);
                await RemoverPessoa(professor);
                return ResultadoServiceFactory<UsuarioVM>.InternalServerError(_sharedResource.ErroInternoServidor);
            }

            return ResultadoServiceFactory<UsuarioVM>.NoContent();
        }

        private Stream RedimensionarImagem(IFormFile arquivoImagem, int altura = 256, int largura = 256)
        {
            using Stream inputStream = arquivoImagem.OpenReadStream();
            Stream outputStream = new MemoryStream();

            try
            {

                using (var image = Image.Load(inputStream))
                {
                    image.Mutate(x => x.Resize(
                        new ResizeOptions
                        {
                            Size = new Size(largura, altura),
                            Mode = ResizeMode.Max,
                        })
                    );

                    image.SaveAsJpeg(outputStream, new JpegEncoder() { Quality = 95 });
                }

                outputStream.Seek(0, SeekOrigin.Begin);

                return outputStream;
            }
            catch (Exception ex)
            {
                _log.LogError(exception: ex, "RedimensionarImagem Error");
                return inputStream; // Retorna a imagem original
            }
        }

        private async Task RemoverUsuario(Usuario user, string role)
        {
            await _userManager.RemoveFromRoleAsync(user, role);
            await _userManager.DeleteAsync(user);
        }

        private async Task RemoverPessoa(Pessoa pessoa)
        {
            _uow.Pessoas.Remove(pessoa);
            await _uow.CompleteAsync();
        }


        #endregion Privates Methods

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
