using LevelLearn.Domain.Institucional;
using LevelLearn.Domain.Pessoas;
using System;

namespace LevelLearn.Domain.Jogo
{
    public class Moeda
    {
        public int MoedaId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public string Motivo { get; set; }
        public decimal MoedasGanha { get; set; }

        public int AlunoId { get; set; }
        public Pessoa Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int? RespostaId { get; set; }
        public Resposta Resposta { get; set; }

        public int? PresencaId { get; set; }
        public Presenca Presenca { get; set; }

        public int? AlunoDesafioId { get; set; }
        public AlunoDesafio AlunoDesafio { get; set; }
    }
}
