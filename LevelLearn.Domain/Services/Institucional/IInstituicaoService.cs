using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Services.Institucional
{
    public interface IInstituicaoService : IServiceBase<Instituicao>, IDisposable
    {
        Task<ResponseAPI> CadastrarInstituicao(Instituicao instituicao);
        Task<ResponseAPI> EditarInstituicao(Guid id, Instituicao instituicao);
    }
}
