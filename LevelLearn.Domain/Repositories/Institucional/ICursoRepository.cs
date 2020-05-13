using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface ICursoRepository : IRepositoryBase<Curso>
    {
        Task<List<Curso>> CursosInstituicaoProfessor(Guid professorId);
        Task<List<Curso>> CursosProfessor(Guid professorId);
        Task<bool> IsProfessorDoCurso(Guid cursoId, Guid pessoaId);

    }
}
