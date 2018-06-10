using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class PresencaRepository : CrudRepository<Presenca>, IPresencaRepository
    {
        public PresencaRepository(DbContext context)
            : base(context)
        { }
    }
}
