using System;

namespace LevelLearn.Domain.Pessoas
{
    public class Notificacao
    {
        public int NotificacaoId { get; set; }
        public string Link { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsVisualizada { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
