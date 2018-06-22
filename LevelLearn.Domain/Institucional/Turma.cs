using LevelLearn.Domain.Pessoas;
using System.Collections.Generic;

namespace LevelLearn.Domain.Institucional
{
    public class Turma
    {
        public int TurmaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public decimal Meta { get; set; } = 0;

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int ProfessorId { get; set; }
        public Pessoa Professor { get; set; }

        public List<AlunoTurma> Alunos { get; set; } = new List<AlunoTurma>();
    }
}
