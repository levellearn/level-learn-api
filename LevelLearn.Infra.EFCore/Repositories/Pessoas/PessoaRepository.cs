﻿using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Repositories.Pessoas;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Infra.EFCore.Repositories.Pessoas
{
    public class PessoaRepository : RepositoryBase<Pessoa, Guid>, IPessoaRepository
    {
        public PessoaRepository(LevelLearnContext context)
            : base(context)
        { }

        //public List<Pessoa> SelectProfessoresInstituicao(int instituicaoId)
        //{
        //    return _context.Set<PessoaInstituicao>()
        //                    .Where(p => p.Perfil == PerfilInstituicaoEnum.Professor || p.Perfil == PerfilInstituicaoEnum.Admin)
        //                    .Where(p => p.InstituicaoId == instituicaoId)
        //                    .Select(p => p.Pessoa)
        //                    .ToList();
        //}

    }
}
