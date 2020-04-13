using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string email, string assunto, string mensagem);
        Task EnviarEmailCadastroProfessor(string email, string nome, string userId, string confirmationToken);
    }
}
