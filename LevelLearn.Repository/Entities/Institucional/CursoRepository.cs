using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class CursoRepository : CrudRepository<Curso>, ICursoRepository
    {
        public CursoRepository(DbContext context)
            : base(context)
        { }
    }
}
