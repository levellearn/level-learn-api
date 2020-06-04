using LevelLearn.Domain.Entities.Comum;
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

        public async Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Instituicao> query = _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor ||
                            p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                    .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);            

            return await query.ToListAsync();
        }

        public async Task<int> TotalInstituicoesProfessor(Guid pessoaId, string filtroPesquisa, bool ativo = true)
        {
            string termoPesquisaSanitizado = filtroPesquisa.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor ||
                       p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == ativo)
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