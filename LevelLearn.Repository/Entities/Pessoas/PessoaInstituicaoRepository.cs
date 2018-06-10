using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class PessoaInstituicaoRepository : CrudRepository<PessoaInstituicao>, IPessoaInstituicaoRepository
    {
        public PessoaInstituicaoRepository(DbContext context)
            : base(context)
        { }
    }
}
