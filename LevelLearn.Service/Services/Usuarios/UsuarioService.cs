using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Pessoas;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        #region Atributos
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly ISharedResource _sharedResource;
        private readonly IValidatorApp<ApplicationUser> _validatorUsuario;
        private readonly IValidatorApp<Professor> _validatorProfessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationUser _usuarioLogado = null;
        #endregion

        #region Ctor
        public UsuarioService(
            IUnitOfWork uow,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IEmailService emailService,
            ISharedResource sharedResource)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _sharedResource = sharedResource;
            _validatorUsuario = new UsuarioValidator(_sharedResource);
            _validatorProfessor = new ProfessorValidator(_sharedResource);
        }
        #endregion

        public async Task<ResponseAPI<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);
            var celular = new Celular(usuarioVM.Celular);
            var cpf = new CPF(usuarioVM.Cpf);

            // Validações BD
            var respostaValidacao = await ValidarCriarUsuarioBD(email.Endereco, cpf.Numero);
            if (!respostaValidacao.Success) return respostaValidacao;

            // Criação e Validação PROFESSOR
            var professor = new Professor(usuarioVM.Nome, usuarioVM.NickName, email, cpf, celular, usuarioVM.Genero, imagemUrl: null, usuarioVM.DataNascimento);

            _validatorProfessor.Validar(professor);
            if (!professor.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(professor.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Criação e Validação USER
            var user = new ApplicationUser(professor.NickName, email.Endereco, emailConfirmed: false, usuarioVM.Senha,
                usuarioVM.ConfirmacaoSenha, celular.Numero, phoneNumberConfirmed: true, professor.Id);

            _validatorUsuario.Validar(user);
            if (!user.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(user.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Criando PROFESSOR BD
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CompleteAsync()) return ResponseFactory<UsuarioVM>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Criando USER Identity
            var role = ApplicationRoles.PROFESSOR;
            var resultadoIdentity = await CriarUsuarioIdentity(user, role, professor);
            if (resultadoIdentity.Failure) return resultadoIdentity;

            // Enviar email de confirmação
            _ = EnviarEmail(professor, user, role);

            // TODO: Remover retorno VM?
            var responseVM = new UsuarioVM()
            {
                Id = user.Id,
                Nome = professor.Nome,
                NickName = user.NickName,
                ImagemUrl = professor.ImagemUrl,
                Token = await _tokenService.GerarJWT(user, new List<string> { role })
            };

            return ResponseFactory<UsuarioVM>.Created(responseVM, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResponseAPI<UsuarioVM>> LogarUsuario(LoginUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);

            // Validações
            switch (usuarioVM.TipoAutenticacao)
            {
                case TipoAutenticacao.Senha:
                    {
                        var respostaValidacao = await ValidarCredenciaisUsuario(email, usuarioVM.Senha);
                        if (respostaValidacao.Failure) return respostaValidacao;
                        break;
                    }
                case TipoAutenticacao.Refresh_Token:
                    {
                        var respostaValidacao = await ValidarRefreshToken(email, usuarioVM.RefreshToken);
                        if (respostaValidacao.Failure) return respostaValidacao;
                        break;
                    }
                default:
                    return ResponseFactory<UsuarioVM>.BadRequest("Tipo de autenticação inválida");
            }

            // Gerar VM e JWToken  
            _usuarioLogado ??= await _userManager.FindByNameAsync(email.Endereco);
            var roles = await _userManager.GetRolesAsync(_usuarioLogado);
            var pessoa = await _uow.Pessoas.GetAsync(_usuarioLogado.PessoaId);

            var responseVM = new UsuarioVM()
            {
                Id = _usuarioLogado.Id,
                Nome = pessoa.Nome,
                NickName = _usuarioLogado.NickName,
                ImagemUrl = pessoa.ImagemUrl,
                Token = await _tokenService.GerarJWT(_usuarioLogado, roles)
            };

            return ResponseFactory<UsuarioVM>.Ok(responseVM, _sharedResource.UsuarioLoginSucesso);
        }

        public async Task<ResponseAPI<UsuarioVM>> Logout(string jwtId)
        {
            await _signInManager.SignOutAsync();
            await _tokenService.InvalidarTokenERefreshTokenCache(jwtId);

            return ResponseFactory<UsuarioVM>.NoContent(_sharedResource.UsuarioLogoutSucesso);
        }

        public async Task<ResponseAPI<UsuarioVM>> ConfirmarEmail(string userId, string confirmationToken)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(confirmationToken))
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.DadosInvalidos);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return ResponseFactory<UsuarioVM>.NotFound(_sharedResource.NaoEncontrado);

            byte[] tokenDecodedBytes = WebEncoders.Base64UrlDecode(confirmationToken);
            string tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);

            var identityResult = await _userManager.ConfirmEmailAsync(user, tokenDecoded);

            if (!identityResult.Succeeded)
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioEmailConfirmarFalha);

            return ResponseFactory<UsuarioVM>.NoContent(_sharedResource.UsuarioEmailConfirmarSucesso);
        }

        #region Privates Methods

        private async Task<ResponseAPI<UsuarioVM>> ValidarCriarUsuarioBD(string email, string cpf)
        {
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == email))
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioEmailJaExiste);

            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == cpf))
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.PessoaCPFJaExiste);

            //if (await _uow.Pessoas.EntityExists(i => i.NickName == pessoa.NickName))
            //    return ResponseFactory<UsuarioVM>.BadRequest("Nickname já existente");

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResponseAPI<UsuarioVM>> ValidarCredenciaisUsuario(Email email, string senha)
        {
            if (!email.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(email.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (string.IsNullOrWhiteSpace(senha))
            {
                var dadoInvalido = new DadoInvalido("Senha", _sharedResource.UsuarioSenhaObrigatoria);
                return ResponseFactory<UsuarioVM>.BadRequest(dadoInvalido, _sharedResource.DadosInvalidos);
            }

            var senhaTamanhoMin = RegraAtributo.Pessoa.SENHA_TAMANHO_MIN;
            var senhaTamanhoMax = RegraAtributo.Pessoa.SENHA_TAMANHO_MAX;

            if (senha.Length < senhaTamanhoMin || senha.Length > senhaTamanhoMax)
            {
                var dadoInvalido = new DadoInvalido("Senha", _sharedResource.UsuarioSenhaTamanho(senhaTamanhoMin, senhaTamanhoMax));
                return ResponseFactory<UsuarioVM>.BadRequest(dadoInvalido, _sharedResource.DadosInvalidos);
            }

            // Sign in Identity            
            _usuarioLogado = await _userManager.FindByNameAsync(email.Endereco);
            if (_usuarioLogado == null)
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioLoginFalha);

            // TODO: Tentar reenviar email?
            if (!_usuarioLogado.EmailConfirmed && await _userManager.CheckPasswordAsync(_usuarioLogado, senha))
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioEmailNaoConfirmado);

            var result = await _signInManager.CheckPasswordSignInAsync(_usuarioLogado, senha, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioContaBloqueada);           

            if (!result.Succeeded)
                return ResponseFactory<UsuarioVM>.BadRequest(_sharedResource.UsuarioLoginFalha);

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResponseAPI<UsuarioVM>> ValidarRefreshToken(Email email, string refreshToken)
        {
            // Validações
            if (!email.EstaValido())
                return ResponseFactory<UsuarioVM>
                    .BadRequest(email.ResultadoValidacao.GetErrorsResult(), _sharedResource.DadosInvalidos);

            if (string.IsNullOrWhiteSpace(refreshToken))
                return ResponseFactory<UsuarioVM>.BadRequest("Refresh Token precisa estar preenchida");

            // Verifica REFRESH TOKEN no BD cache 
            string tokenArmazenadoCache = await _tokenService.ObterRefreshTokenCache(refreshToken);

            if (string.IsNullOrWhiteSpace(tokenArmazenadoCache))
                return ResponseFactory<UsuarioVM>.BadRequest("Refresh Token expirado ou não existente");

            var refreshTokenData = JsonConvert.DeserializeObject<RefreshTokenData>(tokenArmazenadoCache);

            var credenciaisValidas = (email.Endereco == refreshTokenData.UserName
                                        && refreshToken == refreshTokenData.RefreshToken);

            if (!credenciaisValidas)
                return ResponseFactory<UsuarioVM>.BadRequest("Refresh Token inválido");

            // Remove o REFRESH TOKEN já que um novo será gerado
            await _tokenService.InvalidarRefreshTokenCache(refreshToken);

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResponseAPI<UsuarioVM>> CriarUsuarioIdentity(ApplicationUser user, string role, Pessoa pessoa)
        {
            try
            {
                var identityResult = await _userManager.CreateAsync(user, user.Senha);
                if (!identityResult.Succeeded)
                {
                    await RemoverPessoa(pessoa);
                    return ResponseFactory<UsuarioVM>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
                }

                await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return ResponseFactory<UsuarioVM>.NoContent();
            }
            catch (Exception)
            {
                await RemoverUsuario(user, role);
                await RemoverPessoa(pessoa);
                return ResponseFactory<UsuarioVM>.InternalServerError(_sharedResource.ErroInternoServidor);
            }
        }

        private async Task<ResponseAPI<UsuarioVM>> EnviarEmail(Professor professor, ApplicationUser user, string role)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] confirmationTokenBytes = Encoding.UTF8.GetBytes(confirmationToken);
            string tokenEncoded = WebEncoders.Base64UrlEncode(confirmationTokenBytes);

            try
            {
                await _emailService.EnviarEmailCadastroProfessor(user.Email, professor.Nome, user.Id, tokenEncoded);
            }
            catch (Exception)
            {
                await RemoverUsuario(user, role);
                await RemoverPessoa(professor);
                return ResponseFactory<UsuarioVM>.InternalServerError(_sharedResource.ErroInternoServidor);
            }

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task RemoverUsuario(ApplicationUser user, string role)
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
