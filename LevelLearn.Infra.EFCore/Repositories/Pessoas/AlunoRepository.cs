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
    public class AlunoRepository : RepositoryBase<Aluno, Guid>, IAlunoRepository
    {
        public AlunoRepository(LevelLearnContext context) : base(context)
        {
        }      

        public async Task<Aluno> ObterAlunoCompleto(Guid id)
        {
            return await _context.Set<Aluno>()
                .AsNoTracking()
                .Include(a => a.Cursos)
                    .ThenInclude(c => c.Curso)
                .Include(a => a.Turmas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
       
        public async Task<IEnumerable<Aluno>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Aluno> query = _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.Perfil == TipoPessoa.Aluno
                            && p.CursoId == cursoId)
                .Select(p => p.Pessoa)
                    .OfType<Aluno>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                    .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);
            return await query.ToListAsync();
        }

        public async Task<int> TotalAlunosPorCurso(Guid cursoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            return await _context.Set<PessoaCurso>()
                .AsNoTracking()
                .Where(p => p.Perfil == TipoPessoa.Aluno
                            && p.CursoId == cursoId)
                .Select(p => p.Pessoa)
                    .OfType<Aluno>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .CountAsync();
        }


        public async Task<IEnumerable<Aluno>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            IQueryable<Aluno> query = _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.Perfil == PerfilInstituicao.Aluno && p.InstituicaoId == instituicaoId)
                .Select(p => p.Pessoa)
                    .OfType<Aluno>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPorPagina)
                    .Take(filtro.TamanhoPorPagina);

            query = QueryableExtension.OrderBy(query, filtro.OrdenarPor, filtro.OrdenacaoAscendente);
            return await query.ToListAsync();
        }

        public async Task<int> TotalAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro)
        {
            string termoPesquisaSanitizado = filtro.FiltroPesquisa.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.Perfil == PerfilInstituicao.Aluno && p.InstituicaoId == instituicaoId)
                .Select(p => p.Pessoa)
                    .OfType<Aluno>()
                    .Where(p => p.NomePesquisa.Contains(termoPesquisaSanitizado) &&
                                p.Ativo == filtro.Ativo)
                    .CountAsync();
        }

    }
}
