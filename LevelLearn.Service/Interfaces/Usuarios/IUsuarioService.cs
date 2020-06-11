using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResultadoService<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM);
        Task<ResultadoService<UsuarioTokenVM>> LogarUsuario(LoginUsuarioVM usuarioVM);
        Task<ResultadoService<UsuarioVM>> Logout(string jwtId);
        Task<ResultadoService<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken);
        Task<ResultadoService<UsuarioVM>> AlterarFotoPerfil(string userId, IFormFile arquivo);

    }
}
