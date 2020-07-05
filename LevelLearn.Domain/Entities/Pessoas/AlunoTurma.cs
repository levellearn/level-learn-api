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
        public Pessoa Aluno { get; set; }

        public Guid TurmaId { get; private set; }
        public Turma Turma { get; set; }

        public DateTime DataCadastro { get; private set; }

    }
}
