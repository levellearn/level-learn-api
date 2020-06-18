using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.Repositories.Institucional
{
    public class TurmaRepository : RepositoryBase<Turma, Guid>, ITurmaRepository
    {
        public TurmaRepository(LevelLearnContext context)
            : base(context) { }

        public async Task<IEnumerable<Turma>> TurmasCursoProfessor(Guid cursoId, Guid pessoaId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Turma> query = _context.Set<Turma>()
                .AsNoTracking()
                .Where(p => p.ProfessorId == pessoaId && p.CursoId == cursoId)
                .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                            p.Ativo == filtro.Ativo)
                .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);

            return await query.ToListAsync();
        }

        public async Task<int> TotalTurmasCursoProfessor(Guid cursoId, Guid pessoaId, string filtroPesquisa, bool ativo = true)
        {
            string termoPesquisaSanitizado = filtroPesquisa.GenerateSlug();

            return await _context.Set<Turma>()
                .AsNoTracking()
                .Where(p => p.ProfessorId == pessoaId && 
                            p.CursoId == cursoId &&
                            p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                            p.Ativo == ativo)
                .CountAsync();
        }

        public async Task<IEnumerable<Turma>> TurmasProfessor(Guid pessoaId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Turma> query = _context.Set<Turma>()
                .AsNoTracking()
                .Where(p => p.ProfessorId == pessoaId)
                .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                            p.Ativo == filtro.Ativo)
                .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);

            return await query.ToListAsync();
        }

        public async Task<int> TotalTurmasProfessor(Guid pessoaId, string filtroPesquisa, bool ativo = true)
        {
            string termoPesquisaSanitizado = filtroPesquisa.GenerateSlug();

            return await _context.Set<Turma>()
                .AsNoTracking()
                .Where(p => p.ProfessorId == pessoaId)
                .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                            p.Ativo == ativo)
                .CountAsync();
        }

        public async Task<IEnumerable<Turma>> TurmasAluno(Guid pessoaId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Turma> query = _context.Set<AlunoTurma>()
                .AsNoTracking()
                .Where(p => p.AlunoId == pessoaId)
                .Select(p => p.Turma)
                    .Where(t => t.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                t.Ativo == filtro.Ativo)
                    .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                    .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);

            return await query.ToListAsync();
        }

        public async Task<int> TotalTurmasAluno(Guid pessoaId, string filtroPesquisa, bool ativo = true)
        {
            string termoPesquisaSanitizado = filtroPesquisa.GenerateSlug();

            return await _context.Set<AlunoTurma>()
                .AsNoTracking()
                .Where(p => p.AlunoId == pessoaId)
                .Select(p => p.Turma)
                    .Where(t => t.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                t.Ativo == ativo)
                .CountAsync();
        }

        public async Task<bool> ProfessorPertenceTurma(Guid turmaId, Guid pessoaId)
        {
            return await _context.Set<Turma>()
                .AsNoTracking()
                .Where(p => p.Id == turmaId && p.ProfessorId == pessoaId)
                .AnyAsync();
        }

        public async Task<bool> AlunoPertenceTurma(Guid turmaId, Guid pessoaId)
        {
            return await _context.Set<AlunoTurma>()
                .AsNoTracking()
                .Where(p => p.TurmaId == turmaId && p.AlunoId == pessoaId)
                .AnyAsync();
        }


    }
}
