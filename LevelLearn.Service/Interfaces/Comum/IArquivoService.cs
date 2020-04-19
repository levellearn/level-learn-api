using LevelLearn.Service.Services.Comum;
using System.IO;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Comum
{
    public interface IArquivoService
    {
        Task<string> ObterArquivo(DiretoriosFirebase diretorio, string nomeArquivo);
        Task<string> SalvarArquivo(Stream arquivo, DiretoriosFirebase diretorio, string nomeArquivo);
        Task DeletarArquivo(DiretoriosFirebase diretorio, string nomeArquivo);
    }
}
