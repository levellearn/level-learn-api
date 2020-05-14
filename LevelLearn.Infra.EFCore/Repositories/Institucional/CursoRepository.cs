using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
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

        public override async Task<Curso> GetAsync(Guid id)
        {
            return await _context.Set<Curso>()
                .AsNoTracking()
                .Include(c => c.Instituicao)
                .Include(c => c.Turmas)
                .Include(c => c.Pessoas)
                    .ThenInclude(p => p.Pessoa)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Curso>> CursosProfessor(Guid pessoaId, string searchFilter, int pageNumber, int pageSize)
        {
            searchFilter = searchFilter.GenerateSlug();

            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == TiposPessoa.Professor)
                .Select(p => p.Curso)
                    .Where(p => p.NomePesquisa.Contains(searchFilter) && p.Ativo)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<int> TotalCursosProfessor(Guid pessoaId, string searchFilter)
        {
            searchFilter = searchFilter.GenerateSlug();

            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == TiposPessoa.Professor)
                .Select(p => p.Curso)
                    .Where(p => p.NomePesquisa.Contains(searchFilter) && p.Ativo)
                .CountAsync();
        }

        public async Task<List<Curso>> CursosInstituicaoProfessor(Guid instituicaoId, Guid pessoaId)
        {
            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == TiposPessoa.Professor)
                .Select(p => p.Curso)
                    .Where(c => c.InstituicaoId == instituicaoId)
                    .OrderBy(c => c.Nome)
                .ToListAsync();

            //return await _context.Set<PessoaInstituicao>()
            //    .AsNoTracking()
            //    .Where(p => p.PessoaId == pessoaId && (p.Perfil == PerfisInstituicao.ProfessorAdmin || p.Perfil == PerfisInstituicao.Professor))
            //    .Select(p => p.Instituicao)
            //        .SelectMany(p => p.Cursos)
            //            .Include(p => p.Pessoas)
            //            .Include(p => p.Instituicao)
            //            .OrderBy(p => p.Nome)
            //    .ToListAsync();
        }

        public async Task<bool> IsProfessorDoCurso(Guid cursoId, Guid pessoaId)
        {
            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.CursoId == cursoId && p.Perfil == TiposPessoa.Professor && p.PessoaId == pessoaId)
                .AnyAsync();

            /* return await _context.Set<Curso>()
                .AsNoTracking()
                .Where(p => p.Id == cursoId)
                .SelectMany(p => p.Pessoas)
                    .Where(p => p.Perfil == TiposPessoa.Professor && p.PessoaId == pessoaId)
                .AnyAsync();
            */
        }

    }
}
