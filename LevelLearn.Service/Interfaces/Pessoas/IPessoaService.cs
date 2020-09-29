using LevelLearn.Domain.Entities.Pessoas;
using System;

namespace LevelLearn.Service.Interfaces.Pessoas
{
    public interface IPessoaService : IServiceBase<Pessoa, Guid>, IDisposable
    {
       
    }

}
