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
        Task<ResponseAPI<Instituicao>> ObterInstituicao(Guid instituicaoId, Guid pessoaId);
        Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(Guid pessoaId, PaginationFilterVM filterVM);
        Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, Guid pessoaId);
        Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid instituicaoId, EditarInstituicaoVM instituicaoVM, Guid pessoaId);
        Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid instituicaoId, Guid pessoaId);
    }
}
