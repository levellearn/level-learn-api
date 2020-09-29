using LevelLearn.Domain.Enums;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string email, string assunto, string mensagem);
        Task EnviarEmailCadastro(string email, string nome, string userId, string tokenEncoded, TipoPessoa tipoPessoa);
        Task EnviarEmailRedefinirSenha(string email, string nome, string userId, string resetToken);
    }
}
