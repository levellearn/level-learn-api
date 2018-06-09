using LevelLearn.Domain.Enum;

namespace LevelLearn.Domain.Jogo
{
    public class Desafio
    {
        public int DesafioId { get; set; }
        public string Nome { get; set; }
        public decimal Moedas { get; set; }
        public PedraDesafioEnum Pedra { get; set; }
        public string Imagem { get; set; }
        public bool IsCompletaUmaVez { get; set; }
    }
}
