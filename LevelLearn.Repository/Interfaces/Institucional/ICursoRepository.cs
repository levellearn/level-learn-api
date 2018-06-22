using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using System.Collections.Generic;

namespace LevelLearn.Repository.Interfaces.Institucional
{
    public interface ICursoRepository : ICrudRepository<Curso>
    {
        List<Curso> CursosInstituicaoProfessor(int professorId);
        List<Curso> CursosProfessor(int professorId);
        bool IsProfessorDoCurso(int cursoId, int pessoaId);
    }
}
