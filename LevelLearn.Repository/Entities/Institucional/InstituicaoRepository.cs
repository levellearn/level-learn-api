using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class InstituicaoRepository : CrudRepository<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(DbContext context)
            : base(context)
        { }

        public bool IsAdmin(int instituicaoId, int pessoaId)
        {
            return _context.Set<Instituicao>()
                            .Where(p => p.InstituicaoId == instituicaoId)
                            .Include(p => p.Pessoas)
                            .AsNoTracking()
                            .FirstOrDefault()
                            .Pessoas.Where(p => p.Perfil == PerfilInstituicaoEnum.Admin && p.PessoaId == pessoaId).Count() > 0;
        }
    }
}
