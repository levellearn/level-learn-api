using LevelLearn.Domain.Entities.Pessoas;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Turma
    {
        //public int TurmaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Meta { get; set; } = 0;

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        //public ICollection<AlunoTurma> Alunos { get; set; } = new List<AlunoTurma>();

    }
}
