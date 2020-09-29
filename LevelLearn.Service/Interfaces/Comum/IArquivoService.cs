using LevelLearn.Service.Services.Comum;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Comum
{
    public interface IArquivoService
    {
        Task<string> ObterArquivo(DiretoriosFirebase diretorio, string nomeArquivo);
        Task<string> SalvarArquivo(Stream arquivo, DiretoriosFirebase diretorio, string nomeArquivo);
        Task DeletarArquivo(DiretoriosFirebase diretorio, string nomeArquivo);
        Stream RedimensionarImagem(IFormFile arquivoImagem, int altura = 256, int largura = 256);
    }
}
