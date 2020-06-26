using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
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
using System.Collections.Generic;
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

        public async Task<ResultadoService<Usuario>> RegistrarProfessor(Professor professor, Usuario usuario)
        {
            // Validação professor
            if (!professor.EstaValido())
                return ResultadoServiceFactory<Usuario>.BadRequest(professor.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação usuário
            usuario.AtribuirPessoaId(professor.Id);

            if (!usuario.EstaValido())
                return ResultadoServiceFactory<Usuario>.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validações BD
            ResultadoService<Usuario> resultadoValidacao = await ValidarCriarUsuarioBD(professor);
            if (resultadoValidacao.Falhou) return resultadoValidacao;

            // Criando professor BD
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando usuário Identity
            string role = ApplicationRoles.PROFESSOR;
            ResultadoService<Usuario> resultadoIdentity = await CriarUsuarioIdentity(usuario, role, professor);
            if (resultadoIdentity.Falhou) return resultadoIdentity;

            // Email de confirmação
            _ = EnviarEmailCadastro(professor, usuario, role);

            return ResultadoServiceFactory<Usuario>.Created(usuario, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<Usuario>> RegistrarAluno(Aluno aluno, Usuario usuario)
        {
            // Validação aluno
            if (!aluno.EstaValido())
                return ResultadoServiceFactory<Usuario>.BadRequest(aluno.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação usuário
            usuario.AtribuirPessoaId(aluno.Id);

            if (!usuario.EstaValido())
                return ResultadoServiceFactory<Usuario>.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validações BD
            ResultadoService<Usuario> resultadoValidacao = await ValidarCriarUsuarioBD(aluno);
            if (resultadoValidacao.Falhou) return resultadoValidacao;

            // Criando aluno BD
            await _uow.Pessoas.AddAsync(aluno);
            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando usuário Identity
            string role = ApplicationRoles.ALUNO;
            ResultadoService<Usuario> resultadoIdentity = await CriarUsuarioIdentity(usuario, role, aluno);
            if (resultadoIdentity.Falhou) return resultadoIdentity;

            // Email de confirmação
            _ = EnviarEmailCadastro(aluno, usuario, role);

            return ResultadoServiceFactory<Usuario>.Created(usuario, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> LoginEmailSenha(string email, string senha)
        {
            var emailVO = new Email(email);

            ResultadoService<UsuarioTokenVM> resultadoValidacao = ValidarEmailSenha(emailVO, senha);
            if (resultadoValidacao.Falhou) return resultadoValidacao;

            // Sign in Identity            
            Usuario usuario = await _userManager.FindByNameAsync(emailVO.Endereco);
            if (usuario == null)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            if (!usuario.EmailConfirmed && await _userManager.CheckPasswordAsync(usuario, senha))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioEmailNaoConfirmado);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(usuario, senha, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioContaBloqueada);

            if (!result.Succeeded)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            // Gerar VM e JWToken  
            IList<string> roles = await _userManager.GetRolesAsync(usuario);

            var responseVM = new UsuarioTokenVM()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                NickName = usuario.NickName,
                ImagemUrl = usuario.ImagemUrl,
                Token = await _tokenService.GerarJWT(usuario, roles)
            };

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioLoginSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> LoginRefreshToken(string email, string refreshToken)
        {
            var emailVO = new Email(email);

            ResultadoService<UsuarioTokenVM> resultadoValidacao = ValidarRefreshToken(emailVO, refreshToken);
            if (resultadoValidacao.Falhou) return resultadoValidacao;

            // Verifica REFRESH TOKEN no BD cache 
            string tokenArmazenadoCache = await _tokenService.ObterRefreshTokenCache(refreshToken);

            if (string.IsNullOrWhiteSpace(tokenArmazenadoCache))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token expirado ou não existente");

            var refreshTokenData = JsonConvert.DeserializeObject<RefreshTokenData>(tokenArmazenadoCache);

            bool credenciaisValidas = (emailVO.Endereco == refreshTokenData.Email && refreshToken == refreshTokenData.RefreshToken);

            if (!credenciaisValidas)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token inválido");

            // Remove o REFRESH TOKEN já que um novo será gerado
            await _tokenService.InvalidarRefreshTokenCache(refreshToken);

            // Gerar VM e JWToken  
            Usuario usuario = await _userManager.FindByNameAsync(emailVO.Endereco);

            if (usuario == null) return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            IList<string> roles = await _userManager.GetRolesAsync(usuario);

            var responseVM = new UsuarioTokenVM()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                NickName = usuario.NickName,
                ImagemUrl = usuario.ImagemUrl,
                Token = await _tokenService.GerarJWT(usuario, roles)
            };

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioLoginSucesso);
        }

        public async Task<ResultadoService<Usuario>> Logout(string jwtId)
        {
            await _signInManager.SignOutAsync();
            await _tokenService.InvalidarTokenERefreshTokenCache(jwtId);

            return ResultadoServiceFactory<Usuario>.NoContent(_usuarioResource.UsuarioLogoutSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(confirmationToken))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_sharedResource.DadosInvalidos);

            Usuario usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null) return ResultadoServiceFactory<UsuarioTokenVM>.NotFound(_sharedResource.NaoEncontrado);

            string tokenDecoded = confirmationToken.DecodeBase64ToText();

            // Verificando confirmação Email Identity
            IdentityResult identityResult = await _userManager.ConfirmEmailAsync(usuario, tokenDecoded);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioEmailConfirmarFalha);

            IList<string> roles = await _userManager.GetRolesAsync(usuario);

            var responseVM = new UsuarioTokenVM()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                NickName = usuario.NickName,
                ImagemUrl = usuario.ImagemUrl,
                Token = await _tokenService.GerarJWT(usuario, roles)
            };

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioEmailConfirmarSucesso);
        }

        public async Task<ResultadoService<Usuario>> AlterarFotoPerfil(string userId, IFormFile arquivo)
        {
            Usuario usuario = await _userManager.FindByIdAsync(userId);

            // Validações BD
            if (usuario == null) return ResultadoServiceFactory<Usuario>.NotFound(_sharedResource.NaoEncontrado);

            // Validações Arquivo
            const int TAMANHO_MAXIMO_BYTES = 5_000_000; // 5mb
            DiretoriosFirebase diretorio = DiretoriosFirebase.ImagensPerfilUsuario;
            var mimeTypesAceitos = new string[] { "image/jpeg", "image/png", "image/gif" };

            if (arquivo == null || arquivo.Length <= 0 || arquivo.Length > TAMANHO_MAXIMO_BYTES)
                return ResultadoServiceFactory<Usuario>.BadRequest(_sharedResource.DadosInvalidos);

            if (!mimeTypesAceitos.Any(m => m == arquivo.ContentType))
                return ResultadoServiceFactory<Usuario>.BadRequest(_sharedResource.DadosInvalidos);

            Stream imagemRedimensionada = RedimensionarImagem(arquivo);

            // Upload Firebase Storage
            string nomeArquivo = usuario.GerarNomeFotoPerfil();

            string imagemUrl = await _arquivoService.SalvarArquivo(imagemRedimensionada, diretorio, nomeArquivo);

            string nomeImagemAnterior = usuario.AlterarFotoPerfil(imagemUrl, nomeArquivo);

            _ = _arquivoService.DeletarArquivo(diretorio, nomeImagemAnterior);

            // Atualizando USUÁRIO BD
            IdentityResult identityResult = await _userManager.UpdateAsync(usuario);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory<Usuario>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);

            return ResultadoServiceFactory<Usuario>.Ok(usuario, _sharedResource.AtualizadoSucesso);
        }


        #region Privates Methods

        private async Task<ResultadoService<Usuario>> ValidarCriarUsuarioBD(Pessoa pessoa)
        {
            if (pessoa.TipoPessoa == TipoPessoa.Professor)
            {
                if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == pessoa.Cpf.Numero))
                    return ResultadoServiceFactory<Usuario>.BadRequest(_pessoaResource.PessoaCPFJaExiste);
            }

            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == pessoa.Email.Endereco))
                return ResultadoServiceFactory<Usuario>.BadRequest(_usuarioResource.UsuarioEmailJaExiste);

            return ResultadoServiceFactory<Usuario>.NoContent();
        }

        private async Task<ResultadoService<Usuario>> CriarUsuarioIdentity(Usuario usuario, string role, Pessoa pessoa)
        {
            try
            {
                var identityResult = await _userManager.CreateAsync(usuario, usuario.Senha);
                if (!identityResult.Succeeded)
                {
                    await RemoverPessoa(pessoa);
                    return ResultadoServiceFactory<Usuario>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
                }

                await _userManager.AddToRoleAsync(usuario, role);
                await _signInManager.SignInAsync(usuario, isPersistent: false);

                return ResultadoServiceFactory<Usuario>.NoContent();
            }
            catch (Exception ex)
            {
                _log.LogError(exception: ex, "Erro Criar Usuário Identity");

                await RemoverUsuario(usuario, role);
                await RemoverPessoa(pessoa);
                return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.ErroInternoServidor);
            }
        }

        private async Task<ResultadoService<Usuario>> EnviarEmailCadastro(Pessoa pessoa, Usuario usuario, string role)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            string tokenEncoded = confirmationToken.EncodeTextToBase64();

            try
            {
                await _emailService.EnviarEmailCadastro(usuario.Email, pessoa.Nome, usuario.Id, tokenEncoded, pessoa.TipoPessoa);
            }
            catch (Exception ex)
            {
                _log.LogError(exception: ex, "Erro Enviar Email");

                await RemoverUsuario(usuario, role);
                await RemoverPessoa(pessoa);
                return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.ErroInternoServidor);
            }

            return ResultadoServiceFactory<Usuario>.NoContent();
        }

        private ResultadoService<UsuarioTokenVM> ValidarEmailSenha(Email email, string senha)
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

            return ResultadoServiceFactory<UsuarioTokenVM>.NoContent();
        }

        private ResultadoService<UsuarioTokenVM> ValidarRefreshToken(Email email, string refreshToken)
        {
            if (!email.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>
                    .BadRequest(email.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (string.IsNullOrWhiteSpace(refreshToken))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token precisa estar preenchida");

            return ResultadoServiceFactory<UsuarioTokenVM>.NoContent();
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
                _log.LogError(exception: ex, "Erro Redimensionar Imagem");
                return inputStream; // Retorna a imagem original
            }
        }

        private async Task RemoverUsuario(Usuario usuario, string role)
        {
            await _userManager.RemoveFromRoleAsync(usuario, role);
            await _userManager.DeleteAsync(usuario);
        }

        private async Task RemoverPessoa(Pessoa pessoa)
        {
            _uow.Pessoas.Remove(pessoa);
            await _uow.CommitAsync();
        }


        #endregion Privates Methods

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
