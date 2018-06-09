using LevelLearn.Domain.Enum;

namespace LevelLearn.Domain.Jogo
{
    public class Resposta
    {
        public int RespostaId { get; set; }
        public int TimeId { get; set; }
        public Time Time { get; set; }

        public string RespostaMissao { get; set; }
        public decimal Nota { get; set; }
        public StatusRespostaEnum Status { get; set; }
        public decimal MoedasGanhas { get; set; }

        public int MissaoId { get; set; }
        public Missao Missao { get; set; }
    }
}
