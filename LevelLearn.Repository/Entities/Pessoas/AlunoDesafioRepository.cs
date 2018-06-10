using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class AlunoDesafioRepository : CrudRepository<AlunoDesafio>, IAlunoDesafioRepository
    {
        public AlunoDesafioRepository(DbContext context)
            : base(context)
        { }
    }
}
