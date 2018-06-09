using LevelLearn.Domain.Institucional;

namespace LevelLearn.Domain.Pessoas
{
    public class AlunoTurma
    {
        public int AlunoTurmaId { get; set; }

        public int AlunoId { get; set; }
        public Pessoa Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
