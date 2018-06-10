using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class TimeRepository : CrudRepository<Time>, ITimeRepository
    {
        public TimeRepository(DbContext context)
            : base(context)
        { }
    }
}
