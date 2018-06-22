using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class CursoRepository : CrudRepository<Curso>, ICursoRepository
    {
        public CursoRepository(DbContext context)
            : base(context)
        { }

        public bool IsProfessor(int cursoId, int pessoaId)
        {
            return _context.Set<Curso>()
                            .Where(p => p.CursoId == cursoId)
                            .SelectMany(p => p.Pessoas)
                            .Where(p => p.Perfil == TipoPessoaEnum.Professor && p.PessoaId == pessoaId)
                            .Count() > 0;
        }
    }
}
