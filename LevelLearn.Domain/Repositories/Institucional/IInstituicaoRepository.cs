using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface IInstituicaoRepository : IRepositoryBase<Instituicao>
    {
        bool IsAdmin(Guid instituicaoId, Guid pessoaId);
        Task<List<Instituicao>> InstituicoesAdmin(Guid pessoaId);
        Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId);
        Task<List<Instituicao>> InstituicoesAluno(Guid pessoaId);
    }
}
