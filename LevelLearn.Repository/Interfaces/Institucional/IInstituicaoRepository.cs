using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using System.Collections.Generic;

namespace LevelLearn.Repository.Interfaces.Institucional
{
    public interface IInstituicaoRepository : ICrudRepository<Instituicao>
    {
        bool IsAdmin(int instituicaoId, int pessoaId);
        List<Instituicao> InstituicoesAdmin(int pessoaId);
        List<Instituicao> InstituicoesProfessor(int pessoaId);
        List<Instituicao> InstituicoesAluno(int pessoaId);
    }
}
