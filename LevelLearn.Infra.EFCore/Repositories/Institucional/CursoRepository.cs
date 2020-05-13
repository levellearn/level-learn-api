using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.Repositories.Institucional
{
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public CursoRepository(LevelLearnContext context)
            : base(context)
        { }

        public async Task<List<Curso>> CursosInstituicaoProfessor(Guid professorId)
        {
            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == professorId && (p.Perfil == PerfisInstituicao.ProfessorAdmin || p.Perfil == PerfisInstituicao.Professor))
                .Select(p => p.Instituicao)
                .SelectMany(p => p.Cursos)
                .Include(p => p.Pessoas)
                .Include(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<List<Curso>> CursosProfessor(Guid professorId)
        {
            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.PessoaId == professorId && p.Perfil == TiposPessoa.Professor)
                .Select(p => p.Curso)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<bool> IsProfessorDoCurso(Guid cursoId, Guid pessoaId)
        {
            return await _context.Set<Curso>()
                .AsNoTracking()
                .Where(p => p.Id == cursoId)
                .SelectMany(p => p.Pessoas)
                    .Where(p => p.Perfil == TiposPessoa.Professor && p.PessoaId == pessoaId)
                .AnyAsync();
        }


    }
}
