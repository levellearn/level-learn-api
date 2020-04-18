using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Comum
{
    public interface IArquivoService
    {
        Task<string> ObterImagem(string nomeArquivo);
        Task<string> ObterArquivo(string nomeArquivo);
        Task<string> SalvarArquivo(IFormFile formFile, string diretorio);
        Task DeletarArquivo();
    }
}
