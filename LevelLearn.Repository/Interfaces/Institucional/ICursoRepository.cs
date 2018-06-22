using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;

namespace LevelLearn.Repository.Interfaces.Institucional
{
    public interface ICursoRepository : ICrudRepository<Curso>
    {
        bool IsProfessor(int cursoId, int pessoaId);
    }
}
