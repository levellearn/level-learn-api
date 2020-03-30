using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResponseAPI<UsuarioVM>> RegistrarUsuario(RegistrarUsuarioVM usuarioVM);
        Task<ResponseAPI<UsuarioVM>> LogarUsuario(LoginUsuarioVM usuarioVM);
        Task<ResponseAPI<UsuarioVM>> Logout(string jwtId);

    }
}
