using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Comum
{
    public interface IArquivoService
    {
        Task<string> ObterArquivo(string dir, string nomeArquivo);
        Task SalvarArquivo(IFormFile formFile, string dir);
        Task DeletarArquivo();
    }
}
