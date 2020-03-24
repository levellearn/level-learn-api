using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(
            IUnitOfWork uow,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseAPI<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            // VOs
            var email = new Email(usuarioVM.Email);
            var cpf = new CPF(usuarioVM.Cpf);
            var celular = new Celular(usuarioVM.Celular);

            // Validação Professor
            var professor = new Professor(usuarioVM.Nome, usuarioVM.UserName, email, cpf, celular,
                usuarioVM.Genero, imagemUrl: null, usuarioVM.DataNascimento);

            if (!professor.EstaValido())
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Dados inválidos", professor.DadosInvalidos());

            // Validação Usuário
            var user = new ApplicationUser(usuarioVM.UserName, usuarioVM.Email, emailConfirmed: true, usuarioVM.Senha,
                usuarioVM.ConfirmacaoSenha, usuarioVM.Celular, phoneNumberConfirmed: true, professor.Id);

            if (!user.EstaValido())
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Dados inválidos", user.DadosInvalidos());

            // Validação BD
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == professor.Email.Endereco))
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("E-mail já existente");

            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == professor.Cpf.Numero))
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("CPF já existente");

            //if (await _uow.Pessoas.EntityExists(i => i.NickName == professor.NickName))
            //    return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Nickname já existente");

            // Criando Professor
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CompleteAsync())
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            // Criando User Identity
            try
            {
                var identityResult = await _userManager.CreateAsync(user, usuarioVM.Senha);
                if (!identityResult.Succeeded)
                {
                    await RemoverPessoa(professor);
                    return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Dados inválidos", identityResult.GetErrorsResult());
                }

                await _userManager.AddToRoleAsync(user, ApplicationRoles.PROFESSOR);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            catch (Exception ex)
            {
                await RemoverUsuario(user, ApplicationRoles.PROFESSOR);
                await RemoverPessoa(professor);
                throw ex;
            }

            var responseVM = new UsuarioVM()
            {
                Id = user.Id,
                Nome = professor.Nome,
                NickName = user.NickName,
                ImagemUrl = professor.ImagemUrl,
                Token = await _tokenService.GerarJWT(user, new List<string> { ApplicationRoles.PROFESSOR })
            };

            return ResponseAPI<UsuarioVM>.ResponseAPIFactory.Created(responseVM);
        }

        public async Task<ResponseAPI<UsuarioVM>> LogarUsuario(LoginUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);

            // Validações
            var respostaValidacao = ValidarCredenciaisUsuario(email, usuarioVM.Senha);
            if (!respostaValidacao.Success) return respostaValidacao;

            // SignIn
            var result = await _signInManager.PasswordSignInAsync(
                 email.Endereco, usuarioVM.Senha, isPersistent: false, lockoutOnFailure: true
             );

            if (result.IsLockedOut)
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Sua conta está bloqueada");

            if (!result.Succeeded)
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory.BadRequest("Usuário e/ou senha inválidos");

            // Gerar Token           
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

            return ResponseAPI<UsuarioVM>.ResponseAPIFactory.Ok(responseVM, "Login feito com sucesso");
        }

        public async Task<ResponseAPI<UsuarioVM>> Logout()
        {
            await _signInManager.SignOutAsync();
            return ResponseAPI<UsuarioVM>.ResponseAPIFactory.Ok("Logout feito com sucesso");
        }

        #region Privates Methods

        private ResponseAPI<UsuarioVM> ValidarCredenciaisUsuario(Email email, string senha)
        {
            if (!email.EstaValido())
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory
                    .BadRequest("Dados inválidos", email.ValidationResult.GetErrorsResult());

            if (string.IsNullOrWhiteSpace(senha))
                return ResponseAPI<UsuarioVM>.ResponseAPIFactory
                    .BadRequest("Dados inválidos", new DadoInvalido("Senha", "Senha precisa estar preenchida"));

            return ResponseAPI<UsuarioVM>.ResponseAPIFactory.Ok();
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
        #endregion

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
