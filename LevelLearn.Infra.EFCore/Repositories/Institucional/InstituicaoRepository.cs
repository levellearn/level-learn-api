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
    public class InstituicaoRepository : RepositoryBase<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(LevelLearnContext context)
            : base(context) { }

        public async Task<Instituicao> InstituicaoCompleta(Guid id)
        {
            return await _context.Set<Instituicao>()
                .Include(i => i.Cursos)
                .Include(i => i.Pessoas)
                    .ThenInclude(p => p.Pessoa)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Instituicao>> InstituicoesProfessorAdmin(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId &&
                            p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId, string termoPesquisa, int numeroPagina, int tamanhoPorPagina, string ordernarPor, bool ordenacaoAscendente = true, bool ativo = true)
        {
            string termoPesquisaSanitizado = termoPesquisa.GenerateSlug();

            IQueryable<Instituicao> query = _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor ||
                            p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == ativo)
                    .Skip((numeroPagina - 1) * tamanhoPorPagina)
                    .Take(tamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, ordernarPor, ordenacaoAscendente);            

            return await query.ToListAsync();
        }

        public async Task<int> TotalInstituicoesProfessor(Guid pessoaId, string searchFilter)
        {
            searchFilter = searchFilter.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor ||
                       p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(searchFilter) &&
                                p.Ativo)
                .CountAsync();
        }

        public async Task<List<Instituicao>> InstituicoesAluno(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == PerfisInstituicao.Aluno)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public Task<bool> ProfessorAdmin(Guid instituicaoId, Guid pessoaId)
        {
            return _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId &&
                            p.InstituicaoId == instituicaoId &&
                            p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .AnyAsync();
        }

    }
}