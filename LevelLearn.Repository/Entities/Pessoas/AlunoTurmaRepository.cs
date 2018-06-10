using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class AlunoTurmaRepository : CrudRepository<AlunoTurma>, IAlunoTurmaRepository
    {
        public AlunoTurmaRepository(DbContext context)
            : base(context)
        { }
    }
}
