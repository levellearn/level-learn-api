using LevelLearn.Domain.Pessoas;
using LevelLearn.Service.Base;
using System.Collections.Generic;

namespace LevelLearn.Service.Interfaces.Pessoas
{
    public interface IPessoaService : ICrudService<Pessoa>
    {
        List<Pessoa> SelectAlunosInstituicao(int instituicaoId);
        List<Pessoa> SelectProfessoresInstituicao(int instituicaoId);
        List<Pessoa> SelectAlunosCurso(int cursoId);
    }
}
