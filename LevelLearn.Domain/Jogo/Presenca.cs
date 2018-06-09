using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Pessoas;

namespace LevelLearn.Domain.Jogo
{
    public class Presenca
    {
        public int PresencaId { get; set; }
        public TipoPresencaEnum TipoPresenca { get; set; }
        public decimal MoedasGanha { get; set; }

        public int ChamadaId { get; set; }
        public Chamada Chamada { get; set; }

        public int AlunoId { get; set; }
        public Pessoa Aluno { get; set; }
    }
}
