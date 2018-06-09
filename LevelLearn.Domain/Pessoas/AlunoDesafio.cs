using LevelLearn.Domain.Institucional;
using LevelLearn.Domain.Jogo;
using System;

namespace LevelLearn.Domain.Pessoas
{
    public class AlunoDesafio
    {
        public int AlunoDesafioId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public decimal MoedasGanha { get; set; }

        public int AlunoId { get; set; }
        public Pessoa Aluno { get; set; }

        public int DesafioId { get; set; }
        public Desafio Desafio { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
