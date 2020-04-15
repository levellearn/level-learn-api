using LevelLearn.Service.Interfaces.Comum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Comum
{
    public class ArquivoService : IArquivoService
    {

        public ArquivoService()
        {

        }


        public Task<object> ObterArquivo()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> ObterArquivos()
        {
            throw new NotImplementedException();
        }

        public Task SalvarArquivo()
        {
            throw new NotImplementedException();
        }

        public Task DeletarArquivo()
        {
            throw new NotImplementedException();
        }


    }
}
