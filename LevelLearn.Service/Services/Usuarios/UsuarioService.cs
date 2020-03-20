using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
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

        public async Task<ResponseAPI> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            // VOs
            var email = new Email(usuarioVM.Email);
            var cpf = new CPF(usuarioVM.Cpf);
            var celular = new Celular(usuarioVM.Celular);

            // Validação Professor
            var professor = new Professor(usuarioVM.Nome, usuarioVM.UserName, email, cpf, celular,
                usuarioVM.Genero, imagemUrl: null, usuarioVM.DataNascimento);

            if (!professor.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", professor.DadosInvalidos());

            // Validação Usuário
            var user = new ApplicationUser(usuarioVM.UserName, usuarioVM.Email, emailConfirmed: true, usuarioVM.Senha,
                usuarioVM.ConfirmacaoSenha, usuarioVM.Celular, phoneNumberConfirmed: true, professor.Id);

            if (!user.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", user.DadosInvalidos());

            // Validação BD
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == professor.Email.Endereco))
                return ResponseAPI.ResponseAPIFactory.BadRequest("E-mail já existente");

            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == professor.Cpf.Numero))
                return ResponseAPI.ResponseAPIFactory.BadRequest("CPF já existente");

            // Criando Professor
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");
            
            // Criando User Identity
            var identityResult = await _userManager.CreateAsync(user, usuarioVM.Senha);

            if (!identityResult.Succeeded)
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", identityResult.GetErrorsResult());

            var role = PerfisInstituicao.Professor;
            await _userManager.AddToRoleAsync(user, role.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);

            // remover usuário
            
            var responseVM = new UsuarioVM()
            {
                Id = professor.Id.ToString(),
                Nome = professor.Nome,
                UserName = professor.UserName,
                ImagemUrl = professor.ImagemUrl,
                Token = _tokenService.GerarJWT(professor, new List<PerfisInstituicao>() { role })
            };

            return ResponseAPI.ResponseAPIFactory.Created(responseVM);
        }

        public async Task<ResponseAPI> LogarUsuario(LoginUsuarioVM usuarioVM)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
