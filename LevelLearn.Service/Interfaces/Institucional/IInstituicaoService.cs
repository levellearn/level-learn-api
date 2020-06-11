using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface IInstituicaoService : IServiceBase<Instituicao, Guid>, IDisposable
    {
        Task<ResultadoService<Instituicao>> ObterInstituicao(Guid instituicaoId, Guid pessoaId);
        Task<ResultadoService<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(Guid pessoaId, FiltroPaginacao filtroPaginacao);
        Task<ResultadoService<Instituicao>> CadastrarInstituicao(Instituicao instituicao, Guid pessoaId);
        Task<ResultadoService<Instituicao>> EditarInstituicao(Guid instituicaoId, EditarInstituicaoVM instituicaoVM, Guid pessoaId);
        Task<ResultadoService<Instituicao>> AlternarAtivacao(Guid instituicaoId, Guid pessoaId);
    }
}
