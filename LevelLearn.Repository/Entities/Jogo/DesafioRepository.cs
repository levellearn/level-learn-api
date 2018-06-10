using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class DesafioRepository : CrudRepository<Desafio>, IDesafioRepository
    {
        public DesafioRepository(DbContext context)
            : base(context)
        { }
    }
}
