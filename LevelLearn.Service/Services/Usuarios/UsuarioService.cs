using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly IDistributedCache _redisCache;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(
            IUnitOfWork uow,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IDistributedCache redisCache)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _redisCache = redisCache;
        }

        public async Task<ResponseAPI<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);
            var celular = new Celular(usuarioVM.Celular);
            // Validação Professor
            var professor = new Professor(usuarioVM.Nome, usuarioVM.NickName, email,
                new CPF(usuarioVM.Cpf), celular, usuarioVM.Genero, imagemUrl: null, usuarioVM.DataNascimento);

            if (!professor.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(professor.DadosInvalidos());

            // Validação Usuário
            var user = new ApplicationUser(professor.NickName, email.Endereco, emailConfirmed: true, usuarioVM.Senha,
                usuarioVM.ConfirmacaoSenha, celular.Numero, phoneNumberConfirmed: true, professor.Id);

            if (!user.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(user.DadosInvalidos());

            // Validações BD
            var respostaValidacao = await ValidarCriarUsuarioBD(professor);
            if (!respostaValidacao.Success) return respostaValidacao;

            // Criando Professor
            await _uow.Pessoas.AddAsync(professor);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<UsuarioVM>.InternalServerError("Falha ao salvar");

            // Criando User Identity
            var resultadoIdentity = await CriarUsuarioIdentity(user, ApplicationRoles.PROFESSOR, professor);

            if (!resultadoIdentity.Success) return resultadoIdentity;

            var responseVM = new UsuarioVM()
            {
                Id = user.Id,
                Nome = professor.Nome,
                NickName = user.NickName,
                ImagemUrl = professor.ImagemUrl,
                Token = await _tokenService.GerarJWT(user, new List<string> { ApplicationRoles.PROFESSOR })
            };

            return ResponseFactory<UsuarioVM>.Created(responseVM);
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
                    if (respostaValidacao.Failure)
                        return respostaValidacao;
                    break;
                }
                case TipoAutenticacao.Refresh_Token:
                {
                    var respostaValidacao = await ValidarRefreshToken(email, usuarioVM.RefreshToken);
                    if (respostaValidacao.Failure)
                        return respostaValidacao;
                    break;
                }
                default:
                    return ResponseFactory<UsuarioVM>.BadRequest(new DadoInvalido(
                        nameof(LoginUsuarioVM.TipoAutenticacao), "Tipo de autenticação inválida"));
            }

            // Gerar VM e JWToken           
            var user = await _userManager.FindByNameAsync(email.Endereco);
            var roles = await _userManager.GetRolesAsync(user);
            var pessoa = await _uow.Pessoas.GetAsync(user.PessoaId);

            var responseVM = new UsuarioVM()
            {
                Id = user.Id,
                Nome = pessoa.Nome,
                NickName = user.NickName,
                ImagemUrl = pessoa.ImagemUrl,
                Token = await _tokenService.GerarJWT(user, roles)
            };

            return ResponseFactory<UsuarioVM>.Ok(responseVM, "Login feito com sucesso");
        }

        public async Task<ResponseAPI<UsuarioVM>> Logout(string jwtId)
        {
            await _signInManager.SignOutAsync();

            // Invalida token e refresh token
            var refreshToken = await _redisCache.GetStringAsync(jwtId);
            await _redisCache.RemoveAsync(jwtId);
            await _redisCache.RemoveAsync(refreshToken);

            return ResponseFactory<UsuarioVM>.NoContent("Logout feito com sucesso");
        }


        #region Privates Methods

        private async Task<ResponseAPI<UsuarioVM>> ValidarCriarUsuarioBD(Pessoa pessoa)
        {
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == pessoa.Email.Endereco))
                return ResponseFactory<UsuarioVM>.BadRequest("E-mail já existente");

            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == pessoa.Cpf.Numero))
                return ResponseFactory<UsuarioVM>.BadRequest("CPF já existente");

            //if (await _uow.Pessoas.EntityExists(i => i.NickName == pessoa.NickName))
            //    return ResponseFactory<UsuarioVM>.BadRequest("Nickname já existente");

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResponseAPI<UsuarioVM>> ValidarCredenciaisUsuario(Email email, string senha)
        {
            if (!email.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(email.ValidationResult.GetErrorsResult());

            if (string.IsNullOrWhiteSpace(senha))
                return ResponseFactory<UsuarioVM>.BadRequest(new DadoInvalido("Senha", "Senha precisa estar preenchida"));
            
            if (senha.Length < PropertiesConfig.Pessoa.SENHA_TAMANHO_MIN || senha.Length > PropertiesConfig.Pessoa.SENHA_TAMANHO_MAX)
                return ResponseFactory<UsuarioVM>.BadRequest(new DadoInvalido("Senha", "Senha com tamanho inválido"));

            // Sign in Identity
            var result = await _signInManager.PasswordSignInAsync(
                 email.Endereco, senha, isPersistent: false, lockoutOnFailure: true
             );

            if (result.IsLockedOut)
                return ResponseFactory<UsuarioVM>.BadRequest("Sua conta está bloqueada");

            if (!result.Succeeded)
                return ResponseFactory<UsuarioVM>.BadRequest("Usuário e/ou senha inválidos");

            return ResponseFactory<UsuarioVM>.NoContent();
        }

        private async Task<ResponseAPI<UsuarioVM>> ValidarRefreshToken(Email email, string refreshToken)
        {
            if (!email.EstaValido())
                return ResponseFactory<UsuarioVM>.BadRequest(email.ValidationResult.GetErrorsResult());

            if (string.IsNullOrWhiteSpace(refreshToken))
                return ResponseFactory<UsuarioVM>.BadRequest(new DadoInvalido("RefreshToken", "Refresh Token precisa estar preenchida"));

            string tokenArmazenadoCache = await _redisCache.GetStringAsync(refreshToken);

            if (string.IsNullOrWhiteSpace(tokenArmazenadoCache))
                return ResponseFactory<UsuarioVM>.BadRequest("Refresh Token expirado ou não existente");

            var refreshTokenBase = JsonConvert.DeserializeObject<RefreshTokenData>(tokenArmazenadoCache);

            var credenciaisValidas = (email.Endereco == refreshTokenBase.UserName &&
                refreshToken == refreshTokenBase.RefreshToken);

            if (!credenciaisValidas)
                return ResponseFactory<UsuarioVM>.BadRequest("Refresh Token inválido");

            // Remove o refresh token já que um novo será gerado
            await _redisCache.RemoveAsync(refreshToken);

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
                    return ResponseFactory<UsuarioVM>.BadRequest(identityResult.GetErrorsResult());
                }

                await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return ResponseFactory<UsuarioVM>.NoContent();
            }
            catch (Exception ex)
            {
                await RemoverUsuario(user, role);
                await RemoverPessoa(pessoa);
                throw ex;
            }
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
