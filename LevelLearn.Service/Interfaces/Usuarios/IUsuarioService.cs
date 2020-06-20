using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResultadoService<UsuarioVM>> RegistrarProfessor(Professor professor, Usuario usuario);
        Task<ResultadoService<UsuarioTokenVM>> LogarUsuario(LoginUsuarioVM loginUsuarioVM);
        Task<ResultadoService<UsuarioVM>> Logout(string jwtId);
        Task<ResultadoService<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken);
        Task<ResultadoService<UsuarioVM>> AlterarFotoPerfil(string userId, IFormFile arquivo);

    }
}
