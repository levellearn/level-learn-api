using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class InstituicaoRepository : CrudRepository<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(DbContext context)
            : base(context)
        { }
    }
}
