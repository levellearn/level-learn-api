using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Base;
using System.Collections.Generic;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ICursoService : ICrudService<Curso>
    {
        List<StatusResponseEnum> ValidaCurso(Curso curso);
        bool Insert(Curso curso, List<int> professores, List<int> alunos);
        bool IsProfessor(int cursoId, int pessoaId);
    }
}
