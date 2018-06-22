using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Institucional;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Repository.Entities.Institucional
{
    public class InstituicaoRepository : CrudRepository<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(DbContext context)
            : base(context)
        { }

        public List<Instituicao> InstituicoesAdmin(int pessoaId)
        {
            return _context.Set<PessoaInstituicao>()
                .Where(p => p.Perfil == PerfilInstituicaoEnum.Admin)
                .Where(p => p.PessoaId == pessoaId)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Instituicao> InstituicoesAluno(int pessoaId)
        {
            return _context.Set<PessoaInstituicao>()
                .Where(p => p.Perfil == PerfilInstituicaoEnum.Aluno)
                .Where(p => p.PessoaId == pessoaId)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Instituicao> InstituicoesProfessor(int pessoaId)
        {
            return _context.Set<PessoaInstituicao>()
                .Where(p => p.Perfil == PerfilInstituicaoEnum.Professor || p.Perfil == PerfilInstituicaoEnum.Admin)
                .Where(p => p.PessoaId == pessoaId)
                .Select(p => p.Instituicao)
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public bool IsAdmin(int instituicaoId, int pessoaId)
        {
            return _context.Set<Instituicao>()
                            .Where(p => p.InstituicaoId == instituicaoId)
                            .Include(p => p.Pessoas)
                            .AsNoTracking()
                            .FirstOrDefault()
                            .Pessoas.Where(p => p.Perfil == PerfilInstituicaoEnum.Admin && p.PessoaId == pessoaId).Count() > 0;
        }
    }
}
