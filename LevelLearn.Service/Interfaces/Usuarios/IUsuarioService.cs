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
        Task<ResultadoService<Usuario>> RegistrarProfessor(Professor professor, Usuario usuario);
        Task<ResultadoService<Usuario>> RegistrarAluno(Aluno aluno, Usuario usuario);
        Task<ResultadoService<UsuarioTokenVM>> LoginEmailSenha(string email, string senha);
        Task<ResultadoService<UsuarioTokenVM>> LoginRefreshToken(string email, string refreshToken);
        Task<ResultadoService> Logout(string jwtId);
        Task<ResultadoService<UsuarioTokenVM>> ConfirmarEmail(string userId, string confirmationToken);
        Task<ResultadoService> EsqueciSenha(string email);
        Task<ResultadoService> RedefinirSenha(RedefinirSenhaVM redefinirSenhaVM);
        Task<ResultadoService> AlterarSenha(string userId, AlterarSenhaVM alterarSenhaVM);
        Task<ResultadoService<Usuario>> AlterarFotoPerfil(string userId, IFormFile arquivo);

    }
}
