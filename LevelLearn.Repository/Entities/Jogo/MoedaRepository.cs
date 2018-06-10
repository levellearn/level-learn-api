using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class MoedaRepository : CrudRepository<Moeda>, IMoedaRepository
    {
        public MoedaRepository(DbContext context)
            : base(context)
        { }
    }
}
