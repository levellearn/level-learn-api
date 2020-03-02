using LevelLearn.Domain.Repositories.Institucional;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.UnityOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IInstituicaoRepository Instituicoes { get; }

        bool Complete();
        Task<bool> CompleteAsync();
    }
}
