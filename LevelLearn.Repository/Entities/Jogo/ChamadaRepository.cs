using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class ChamadaRepository : CrudRepository<Chamada>, IChamadaRepository
    {
        public ChamadaRepository(DbContext context)
            : base(context)
        { }
    }
}
