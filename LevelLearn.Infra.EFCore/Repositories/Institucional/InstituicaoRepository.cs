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

        public async Task<List<Instituicao>> InstituicoesAdmin(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
        public async Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId, string query, int pageNumber, int pageSize)
        {
            query = query.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor || p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(query) && p.Ativo)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(p => p.Nome)
                .ToListAsync();
        }
        public async Task<int> TotalInstituicoesProfessor(Guid pessoaId, string query)
        {
            query = query.GenerateSlug();

            return await _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor || p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .Select(p => p.Instituicao)
                    .Where(p => p.NomePesquisa.Contains(query) && p.Ativo)
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
        public Task<bool> IsAdmin(Guid instituicaoId, Guid pessoaId)
        {
            //return _context.Set<Instituicao>()
            //    .Where(p => p.Id == instituicaoId)
            //    .Include(p => p.Pessoas)
            //    .AsNoTracking()
            //    .FirstOrDefault()
            //    .Pessoas
            //    .Where(p => p.Perfil == PerfisInstituicao.Admin && p.PessoaId == pessoaId)
            //    .Count() > 0;

            return _context.Set<PessoaInstituicao>()
                .AsNoTracking()
                .Where(p => p.PessoaId == pessoaId && 
                            p.InstituicaoId == instituicaoId && 
                            p.Perfil == PerfisInstituicao.ProfessorAdmin)
                .AnyAsync();         
        }
        public override async Task<Instituicao> GetAsync(Guid id)
        {
            return await _context.Instituicoes
                .Include(i => i.Cursos)
                .Include(i => i.Pessoas)
                    .ThenInclude(p => p.Pessoa)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

    }
}
