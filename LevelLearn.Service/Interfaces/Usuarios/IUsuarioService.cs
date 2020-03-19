using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResponseAPI> RegistrarUsuario(RegistrarUsuarioVM usuarioVM);
        Task<ResponseAPI> LogarUsuario(LoginUsuarioVM usuarioVM);

    }
}
