using LevelLearn.Domain.Entities.Institucional;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class AlunoTurma
    {
        protected AlunoTurma() { }

        public AlunoTurma(Guid alunoId, Guid turmaId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
            DataCadastro = DateTime.UtcNow;
        }

        public Guid AlunoId { get; private set; }
        public virtual Pessoa Aluno { get; private set; }

        public Guid TurmaId { get; private set; }
        public virtual Turma Turma { get; private set; }
        public DateTime DataCadastro { get; private set; }

    }
}
