using LevelLearn.Domain.Institucional;
using System;

namespace LevelLearn.Domain.Jogo
{
    public class Missao
    {
        public int MissaoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Objetivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int Moedas { get; set; }
        public bool IsOnline { get; set; }
        public int QuantidadeMaxAlunos { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
