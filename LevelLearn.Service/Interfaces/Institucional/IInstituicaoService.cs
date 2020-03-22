using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface IInstituicaoService : IServiceBase<Instituicao>, IDisposable
    {
        Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM);
        Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM);
        Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid id);
    }
}
