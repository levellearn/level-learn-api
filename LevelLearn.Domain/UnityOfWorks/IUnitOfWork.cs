using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.Repositories.Pessoas;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.UnityOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IInstituicaoRepository Instituicoes { get; }
        IPessoaRepository Pessoas { get; }

        bool Complete();
        Task<bool> CompleteAsync();
    }
}
