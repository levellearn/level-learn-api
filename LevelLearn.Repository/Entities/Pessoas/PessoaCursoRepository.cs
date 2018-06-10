using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class PessoaCursoRepository : CrudRepository<PessoaCurso>, IPessoaCursoRepository
    {
        public PessoaCursoRepository(DbContext context)
            : base(context)
        { }
    }
}
