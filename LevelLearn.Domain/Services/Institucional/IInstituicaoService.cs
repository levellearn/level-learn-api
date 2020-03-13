using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Repositories;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Services.Institucional
{
    public interface IInstituicaoService : IServiceBase<Instituicao>, IDisposable
    {
        Task<ResponseAPI> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM);
        Task<ResponseAPI> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM);
    }
}
