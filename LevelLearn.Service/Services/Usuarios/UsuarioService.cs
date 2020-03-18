using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Usuarios;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Service.Services.Usuarios;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
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
            // VOs
            var email = new Email(usuarioVM.Email);
            var cpf = new CPF(usuarioVM.Cpf);
            var celular = new Celular(usuarioVM.Celular);
            if (!Enum.TryParse(usuarioVM.Genero, true, out Generos genero))
                genero = Generos.Nenhum;

            // Validação Professor
            var professor = new Professor(usuarioVM.Nome, usuarioVM.UserName, email, cpf, celular, genero, 
                imagemUrl: null, usuarioVM.DataNascimento);

            if (!professor.EstaValido()) 
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", professor.DadosInvalidos());

            // Validação Usuário
            var user = new ApplicationUser(usuarioVM.UserName, usuarioVM.Email, emailConfirmed: true, usuarioVM.Senha, 
                usuarioVM.ConfirmacaoSenha, usuarioVM.Celular, phoneNumberConfirmed: true, professor);

            if (!user.EstaValido()) 
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", user.DadosInvalidos());

            // Validação BD
            if (await _uow.Pessoas.EntityExists(i => i.Email.Endereco == professor.Email.Endereco))
                return ResponseAPI.ResponseAPIFactory.BadRequest("E-mail já existente");

            if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == professor.Cpf.Numero))
                return ResponseAPI.ResponseAPIFactory.BadRequest("CPF já existente");

            // Criando User Identity
            var identityResult = await _userManager.CreateAsync(user, usuarioVM.Senha);

            if (!identityResult.Succeeded) 
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", identityResult.GetErrorsResult());

            await _signInManager.SignInAsync(user, isPersistent: false);

            // Criando Professor
            await _uow.Pessoas.AddAsync(professor);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

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
