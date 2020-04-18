using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResponseAPI<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM);
        Task<ResponseAPI<UsuarioTokenVM>> LogarUsuario(LoginUsuarioVM usuarioVM);
        Task<ResponseAPI<UsuarioVM>> Logout(string jwtId);
        Task<ResponseAPI<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken);
        Task<ResponseAPI<UsuarioVM>> AlterarFotoPerfil(string userId, IFormFile formFile);

    }
}
