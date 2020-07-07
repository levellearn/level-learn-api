using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Repositories.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.Repositories.Pessoas
{
    public class ProfessorRepository : RepositoryBase<Professor, Guid>, IProfessorRepository
    {
        public ProfessorRepository(LevelLearnContext context) : base(context)
        {
        }

        public override async Task<Professor> GetAsync(Guid id)
        {
            return await _context.Set<Professor>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Professor> ObterProfessorCompleto(Guid id)
        {
            return await _context.Set<Professor>()
                .AsNoTracking()
                .Include(a => a.Cursos)
                    .ThenInclude(c => c.Curso)
                .Include(a => a.Turmas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Professor>> ObterProfessoresPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Professor> query = _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.Perfil == PerfilInstituicao.ProfessorAdmin || 
                            p.Perfil == PerfilInstituicao.Professor && 
                            p.InstituicaoId == instituicaoId)
                .Select(p => p.Pessoa)
                    .OfType<Professor>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                    .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);
            return await query.ToListAsync();
        }

        public async Task<int> TotalProfessoresPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.Perfil == PerfilInstituicao.ProfessorAdmin ||
                            p.Perfil == PerfilInstituicao.Professor &&
                            p.InstituicaoId == instituicaoId)
                .Select(p => p.Pessoa)
                    .OfType<Professor>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .CountAsync();
        }

    }
}
