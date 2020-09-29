using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Repositories.Pessoas;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repository;
using System;

namespace LevelLearn.Infra.EFCore.Repositories.Pessoas
{
    public class PessoaRepository : RepositoryBase<Pessoa, Guid>, IPessoaRepository
    {
        public PessoaRepository(LevelLearnContext context)
            : base(context)
        { }


    }
}
