using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Base;
using System.Collections.Generic;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ITurmaService : ICrudService<Turma>
    {
        List<StatusResponseEnum> ValidaTurma(Turma turma);
        bool Insert(Turma turma, List<int> alunos);
        bool IsTurmaDoProfessor(int turmaId, int professorId);
    }
}
