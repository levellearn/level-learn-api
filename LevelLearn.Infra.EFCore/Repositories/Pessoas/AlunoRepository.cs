using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Repositories.Pessoas;
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

        public async Task<IEnumerable<Aluno>> ObterAlunosPorCurso(Guid cursoId)
        {
            return await _context.Set<PessoaCurso>()
                            .AsNoTracking()
                            .Where(p => p.Perfil == TipoPessoa.Aluno && p.CursoId == cursoId)
                            .Select(p => p.Pessoa as Aluno)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> ObterAlunosPorInstituicao(Guid instituicaoId)
        {
            return await _context.Set<PessoaInstituicao>()
                            .AsNoTracking()
                            .Where(p => p.Perfil == PerfilInstituicao.Aluno && p.InstituicaoId == instituicaoId)
                            .Select(p => p.Pessoa as Aluno)
                            .ToListAsync();
        }

    }
}
