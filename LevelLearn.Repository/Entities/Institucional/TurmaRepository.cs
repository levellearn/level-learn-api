using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class TurmaRepository : CrudRepository<Turma>, ITurmaRepository
    {
        public TurmaRepository(DbContext context)
            : base(context)
        { }
    }
}
