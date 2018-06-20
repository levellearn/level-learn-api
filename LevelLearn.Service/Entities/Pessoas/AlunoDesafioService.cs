﻿using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class AlunoDesafioService : CrudService<AlunoDesafio>, IAlunoDesafioService
    {
        private readonly IAlunoDesafioRepository _alunoDesafioRepository;
        public AlunoDesafioService(IAlunoDesafioRepository alunoDesafioRepository)
            : base(alunoDesafioRepository)
        {
            _alunoDesafioRepository = alunoDesafioRepository;
        }
    }
}
