using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
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

        private readonly ILogger<UsuarioService> _logger;

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

            _logger = logger;
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
            ResultadoService resultadoValidacao = await ValidarCriarUsuarioBD(usuario, professor);
            if (resultadoValidacao.Falhou) return ResultadoServiceFactory<Usuario>.BadRequest(resultadoValidacao.Mensagem);

            // Criando professor BD
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando usuário Identity
            string role = UserRoles.PROFESSOR;
            ResultadoService resultadoIdentity = await CriarUsuarioIdentity(usuario, role, professor);
            if (resultadoIdentity.Falhou)
                return ResultadoServiceFactory<Usuario>.BadRequest(resultadoIdentity.Erros, _sharedResource.DadosInvalidos);

            // Email de confirmação
            _ = EnviarEmailCadastro(professor, usuario);

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
            ResultadoService resultadoValidacao = await ValidarCriarUsuarioBD(usuario, aluno);
            if (resultadoValidacao.Falhou) return ResultadoServiceFactory<Usuario>.BadRequest(resultadoValidacao.Mensagem);

            // Criando aluno BD
            await _uow.Pessoas.AddAsync(aluno);
            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory<Usuario>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando usuário Identity
            string role = UserRoles.ALUNO;
            ResultadoService resultadoIdentity = await CriarUsuarioIdentity(usuario, role, aluno);
            if (resultadoIdentity.Falhou)
                return ResultadoServiceFactory<Usuario>.BadRequest(resultadoIdentity.Erros, _sharedResource.DadosInvalidos);

            // Email de confirmação
            _ = EnviarEmailCadastro(aluno, usuario);

            return ResultadoServiceFactory<Usuario>.Created(usuario, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> LoginEmailSenha(string email, string senha)
        {
            // Validações
            var emailVO = new Email(email);
            if (!emailVO.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(emailVO.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            var senhaVO = new Senha(senha);
            if (!senhaVO.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(senhaVO.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            // Sign in Identity            
            Usuario usuario = await _userManager.FindByNameAsync(email);
            if (usuario == null)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            if (!usuario.EmailConfirmed && await _userManager.CheckPasswordAsync(usuario, senha))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioEmailNaoConfirmado);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(usuario, senha, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioContaBloqueada);

            if (!result.Succeeded)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);


            UsuarioTokenVM responseVM = await GerarUsuarioToken(usuario);

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioLoginSucesso);
        }

        public async Task<ResultadoService<UsuarioTokenVM>> LoginRefreshToken(string email, string refreshToken)
        {
            // Validações
            var emailVO = new Email(email);

            if (!emailVO.EstaValido())
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(emailVO.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (string.IsNullOrWhiteSpace(refreshToken)) return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token precisa estar preenchido");

            // Verifica REFRESH TOKEN no BD cache 
            string tokenArmazenadoCache = await _tokenService.ObterRefreshTokenCache(refreshToken);

            if (string.IsNullOrWhiteSpace(tokenArmazenadoCache))
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token expirado ou não existente");

            var refreshTokenData = JsonConvert.DeserializeObject<RefreshTokenData>(tokenArmazenadoCache);

            bool credenciaisValidas = (emailVO.Endereco == refreshTokenData.Email && refreshToken == refreshTokenData.RefreshToken);

            if (!credenciaisValidas)
                return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest("Refresh Token inválido");

            // Remove o REFRESH TOKEN já que um novo será gerado
            _ = _tokenService.InvalidarRefreshTokenCache(refreshToken);

            Usuario usuario = await _userManager.FindByEmailAsync(emailVO.Endereco);

            if (usuario == null) return ResultadoServiceFactory<UsuarioTokenVM>.BadRequest(_usuarioResource.UsuarioLoginFalha);

            UsuarioTokenVM responseVM = await GerarUsuarioToken(usuario);

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioLoginSucesso);
        }

        public async Task<ResultadoService> Logout(string jwtId)
        {
            Task taskSignOut = _signInManager.SignOutAsync();
            Task taskCache = _tokenService.InvalidarTokenERefreshTokenCache(jwtId);

            await taskSignOut; await taskCache;

            return ResultadoServiceFactory.NoContent(_usuarioResource.UsuarioLogoutSucesso);
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

            // TODO: gerar token ao confirmar email?
            UsuarioTokenVM responseVM = await GerarUsuarioToken(usuario);

            return ResultadoServiceFactory<UsuarioTokenVM>.Ok(responseVM, _usuarioResource.UsuarioEmailConfirmarSucesso);
        }

        public async Task<ResultadoService> EsqueciSenha(string email)
        {
            // Validações
            var emailVo = new Email(email);
            if (!emailVo.EstaValido())
                return ResultadoServiceFactory.BadRequest(emailVo.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            Usuario usuario = await _userManager.FindByEmailAsync(emailVo.Endereco);

            if (usuario == null) return ResultadoServiceFactory.NotFound(_sharedResource.NaoEncontrado);

            if (!usuario.EmailConfirmed)
                return ResultadoServiceFactory.BadRequest(_usuarioResource.UsuarioEmailNaoConfirmado);

            // Gera token para redefinir senha 
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            string tokenEncoded = resetToken.EncodeTextToBase64();

            _logger.LogInformation("Usuário esqueceu a senha {@UsuarioId}", usuario.Id);

            Task taskEmail = _emailService.EnviarEmailRedefinirSenha(usuario.Email, usuario.Nome, usuario.Id, tokenEncoded);

            return ResultadoServiceFactory.NoContent();
        }

        public async Task<ResultadoService> RedefinirSenha(RedefinirSenhaVM vm)
        {
            // Validações VM
            if (string.IsNullOrWhiteSpace(vm.Id) || string.IsNullOrWhiteSpace(vm.Token))
                return ResultadoServiceFactory.BadRequest(_sharedResource.DadosInvalidos);

            var senhaVO = new Senha(vm.NovaSenha);
            if (!senhaVO.EstaValido())
                return ResultadoServiceFactory.BadRequest(senhaVO.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (vm.NovaSenha != vm.ConfirmacaoSenha)
            {
                var dadoInvalido = new DadoInvalido(nameof(vm.ConfirmacaoSenha), _usuarioResource.UsuarioConfirmacaoSenhaNaoConfere);
                return ResultadoServiceFactory.BadRequest(dadoInvalido, _sharedResource.DadosInvalidos);
            }

            // Validações BD
            Usuario usuario = await _userManager.FindByIdAsync(vm.Id);

            if (usuario == null) return ResultadoServiceFactory.NotFound(_sharedResource.NaoEncontrado);

            if (!usuario.EmailConfirmed)
                return ResultadoServiceFactory.BadRequest(_usuarioResource.UsuarioEmailNaoConfirmado);

            // Identity verificando redefinir senha
            string tokenDecoded = vm.Token.DecodeBase64ToText();

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(usuario, tokenDecoded, vm.NovaSenha);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory.BadRequest(_usuarioResource.UsuarioRedefinirSenhaFalha);

            _logger.LogInformation("Usuário redefiniu a senha {@UsuarioId}", usuario.Id);

            return ResultadoServiceFactory.NoContent(_usuarioResource.UsuarioRedefinirSenhaSucesso);
        }

        public async Task<ResultadoService<Usuario>> AlterarFotoPerfil(string userId, IFormFile arquivo)
        {
            Usuario usuario = await _userManager.FindByIdAsync(userId);

            // Validações BD
            if (usuario == null) return ResultadoServiceFactory<Usuario>.NotFound(_sharedResource.NaoEncontrado);

            // Validações Imagem
            ResultadoService resultadoValidacao = ValidarImagemPerfil(arquivo);
            if (resultadoValidacao.Falhou)
                return ResultadoServiceFactory<Usuario>.BadRequest(resultadoValidacao.Mensagem);

            Stream imagemRedimensionada = _arquivoService.RedimensionarImagem(arquivo);

            // Upload Firebase Storage
            DiretoriosFirebase diretorio = DiretoriosFirebase.ImagensPerfilUsuario;
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

        private async Task<ResultadoService> ValidarCriarUsuarioBD(Usuario usuario, Pessoa pessoa)
        {
            if (pessoa.TipoPessoa == TipoPessoa.Professor)
            {
                if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == pessoa.Cpf.Numero))
                    return ResultadoServiceFactory.BadRequest(_pessoaResource.PessoaCPFJaExiste);
            }

            if (_userManager.FindByEmailAsync(usuario.Email).Result != null)
                return ResultadoServiceFactory.BadRequest(_usuarioResource.UsuarioEmailJaExiste);

            return ResultadoServiceFactory.NoContent();
        }

        private async Task<ResultadoService> CriarUsuarioIdentity(Usuario usuario, string role, Pessoa pessoa)
        {
            try
            {
                var identityResult = await _userManager.CreateAsync(usuario, usuario.Senha);
                if (!identityResult.Succeeded)
                {
                    await RemoverPessoa(pessoa);
                    return ResultadoServiceFactory.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
                }

                await _userManager.AddToRoleAsync(usuario, role);
                await _signInManager.SignInAsync(usuario, isPersistent: false);

                return ResultadoServiceFactory.NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro Criar Usuário Identity");

                await RemoverUsuario(usuario, role);
                await RemoverPessoa(pessoa);
                return ResultadoServiceFactory.InternalServerError(_sharedResource.ErroInternoServidor);
            }
        }

        private async Task<ResultadoService> EnviarEmailCadastro(Pessoa pessoa, Usuario usuario)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            string tokenEncoded = confirmationToken.EncodeTextToBase64();

            try
            {
                await _emailService.EnviarEmailCadastro(usuario.Email, pessoa.Nome, usuario.Id, tokenEncoded, pessoa.TipoPessoa);
                return ResultadoServiceFactory.NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro Enviar Email");
                return ResultadoServiceFactory.InternalServerError(_sharedResource.ErroInternoServidor);
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

        private ResultadoService ValidarImagemPerfil(IFormFile arquivo)
        {
            const int TAMANHO_MAXIMO_BYTES = 5_000_000; // 5mb
            var mimeTypesAceitos = new string[3] {
                "image/jpeg", "image/png", "image/gif"
            };

            if (arquivo == null || arquivo.Length <= 0 || arquivo.Length > TAMANHO_MAXIMO_BYTES)
                return ResultadoServiceFactory.BadRequest(_sharedResource.DadosInvalidos);

            if (!mimeTypesAceitos.Any(m => m == arquivo.ContentType))
                return ResultadoServiceFactory.BadRequest(_sharedResource.DadosInvalidos);

            return ResultadoServiceFactory.NoContent();
        }

        private async Task<UsuarioTokenVM> GerarUsuarioToken(Usuario usuario)
        {
            IList<string> roles = await _userManager.GetRolesAsync(usuario);

            var responseVM = new UsuarioTokenVM()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                NickName = usuario.NickName,
                ImagemUrl = usuario.ImagemUrl,
                Token = await _tokenService.GerarJWT(usuario, roles)
            };
            return responseVM;
        }


        #endregion Privates Methods

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
