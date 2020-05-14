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
        Task<ResponseAPI<Instituicao>> ObterInstituicao(Guid id, string pessoaId);
        Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(string pessoaId, PaginationFilterVM filterVM);
        Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, string pessoaId);
        Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM, string pessoaId);
        Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid id, string pessoaId);
    }
}
