﻿using LevelLearn.Domain.Entities.Pessoas;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Repositories.Pessoas
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa, Guid>
    {
    //    List<Pessoa> SelectAlunosInstituicao(int instituicaoId);
    //    List<Pessoa> SelectProfessoresInstituicao(int instituicaoId);
    //    List<Pessoa> SelectAlunosCurso(int cursoId);
    }
}
