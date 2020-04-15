using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Comum
{
    public interface IArquivoService
    {
        Task<object> ObterArquivo();
        Task<IEnumerable<object>> ObterArquivos();
        Task SalvarArquivo();
        Task DeletarArquivo();
    }
}
