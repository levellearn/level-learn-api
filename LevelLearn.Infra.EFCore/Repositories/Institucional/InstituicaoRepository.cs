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
    public class InstituicaoRepository : RepositoryBase<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(LevelLearnContext context)
            : base(context) { }

        public async Task<List<Instituicao>> InstituicoesAdmin(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == PerfisInstituicao.Admin)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<List<Instituicao>> InstituicoesAluno(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId && p.Perfil == PerfisInstituicao.Aluno)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId)
        {
            return await _context.Set<PessoaInstituicao>()
                .Where(p => p.PessoaId == pessoaId)
                .Where(p => p.Perfil == PerfisInstituicao.Professor || p.Perfil == PerfisInstituicao.Admin)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public bool IsAdmin(Guid instituicaoId, Guid pessoaId)
        {
            return _context.Set<Instituicao>()
                .Where(p => p.Id == instituicaoId)
                .Include(p => p.Pessoas)
                .AsNoTracking()
                .FirstOrDefault()
                .Pessoas
                .Where(p => p.Perfil == PerfisInstituicao.Admin && p.PessoaId == pessoaId)
                .Count() > 0;

            //return _context.Instituicoes
            //    .AsNoTracking()
            //    .Include(p => p.Pessoas)
            //    .SingleOrDefault(p => p.Id == instituicaoId)
            //    .Pessoas
            //    .Any(p => p.PessoaId == pessoaId && p.Perfil == PerfisInstituicao.Admin);
        }


    }
}
