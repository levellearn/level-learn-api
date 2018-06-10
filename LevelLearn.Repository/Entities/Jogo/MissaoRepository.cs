using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class MissaoRepository : CrudRepository<Missao>, IMissaoRepository
    {
        public MissaoRepository(DbContext context)
            : base(context)
        { }
    }
}
