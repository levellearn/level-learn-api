using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using System.Collections.Generic;

namespace LevelLearn.Repository.Interfaces.Pessoas
{
    public interface IPessoaRepository : ICrudRepository<Pessoa>
    {
        List<Pessoa> SelectAlunosInstituicao(int instituicaoId);
        List<Pessoa> SelectProfessoresInstituicao(int instituicaoId);
        List<Pessoa> SelectAlunosCurso(int cursoId);
    }
}
