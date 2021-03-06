﻿using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.Repositories.Pessoas;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.UnityOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IInstituicaoRepository Instituicoes { get; }
        ICursoRepository Cursos { get; }
        ITurmaRepository Turmas { get; }
        IPessoaRepository Pessoas { get; }
        IAlunoRepository Alunos { get; }
        IProfessorRepository Professores { get; }

        bool Commit();
        Task<bool> CommitAsync();
    }
}
