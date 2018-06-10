using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class AlunoTimeRepository : CrudRepository<AlunoTime>, IAlunoTimeRepository
    {
        public AlunoTimeRepository(DbContext context)
            : base(context)
        { }
    }
}
