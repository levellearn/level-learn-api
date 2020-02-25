using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class AlunoTurma
    {
        protected AlunoTurma() { }

        public AlunoTurma(Guid alunoId, Guid turmaId)
        {
            Id = Guid.NewGuid();
            AlunoId = alunoId;
            TurmaId = turmaId;
        }

        public Guid Id { get; private set; }

        public Guid AlunoId { get; private set; }
        public virtual Pessoa Aluno { get; private set; }

        public Guid TurmaId { get; private set; }
        public virtual Turma Turma { get; private set; }

    }
}
