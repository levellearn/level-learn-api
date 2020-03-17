using LevelLearn.ViewModel.Usuarios;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Services.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task<ResponseAPI> RegistrarUsuario(RegistrarUsuarioVM usuarioVM);
        Task<ResponseAPI> LogarUsuario(LoginUsuarioVM usuarioVM);

    }
}
