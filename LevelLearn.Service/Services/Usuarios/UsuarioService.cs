using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Usuarios;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Service.Services.Usuarios;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Auth
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioService(
            IUnitOfWork uow,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ITokenService tokenService)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseAPI> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            var email = new Email(usuarioVM.Email);
            var cpf = new CPF(usuarioVM.Cpf);
            var celular = new Celular(usuarioVM.Celular);

            if (!Enum.TryParse(usuarioVM.Genero, true, out Generos genero))
                genero = Generos.Nenhum;

            var professor = new Professor(usuarioVM.Nome, usuarioVM.UserName, email, cpf, celular, genero, "", usuarioVM.DataNascimento);
            var user = new ApplicationUser(usuarioVM.UserName, usuarioVM.Email, true, usuarioVM.Celular, true, professor);

            if (!professor.EstaValido()) return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", professor.DadosInvalidos());

            // Validação BD
            //if (await _uow.pe.EntityExists(i => i.NomePesquisa == instituicaoNova.NomePesquisa))
            //    return ResponseAPI.ResponseAPIFactory.BadRequest("Instituição já existente");


            //if (!result.Succeeded) return BadRequest(result.Errors);
            var result = await _userManager.CreateAsync(user, usuarioVM.Senha);

            await _signInManager.SignInAsync(user, isPersistent: false);

            var responseVM = new UsuarioVM()
            {
                Id = professor.Id.ToString(),
                Nome = professor.Nome,
                UserName = professor.UserName,
                ImagemUrl = professor.ImagemUrl,
                Token = _tokenService.GerarJWT()
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
