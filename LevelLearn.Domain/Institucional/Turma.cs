using LevelLearn.Domain.Pessoas;

namespace LevelLearn.Domain.Institucional
{
    public class Turma
    {
        public string TurmaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public decimal Meta { get; set; } = 0;

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int ProfessorId { get; set; }
        public Pessoa Professor { get; set; }
    }
}
