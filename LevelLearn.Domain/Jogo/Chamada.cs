using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using System;

namespace LevelLearn.Domain.Jogo
{
    public class Chamada
    {
        public int ChamadaId { get; set; }
        public DateTime DataAula { get; set; }
        public PeriodoEnum Periodo { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
