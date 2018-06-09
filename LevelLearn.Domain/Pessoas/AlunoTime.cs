using LevelLearn.Domain.Jogo;

namespace LevelLearn.Domain.Pessoas
{
    public class AlunoTime
    {
        public int AlunoTimeId { get; set; }
        public bool IsCriador { get; set; }

        public int AlunoId { get; set; }
        public Pessoa Aluno { get; set; }

        public int TimeId { get; set; }
        public Time Time { get; set; }
    }
}