using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface IInstituicaoService : IServiceBase<Instituicao>, IDisposable
    {
        Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(string pessoaId, PaginationQueryVM queryVM);
        Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, string pessoaId);
        Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM, string pessoaId);
        Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid id, string pessoaId);
    }
}
