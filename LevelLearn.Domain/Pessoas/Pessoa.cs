using LevelLearn.Domain.Enum;
using System;

namespace LevelLearn.Domain.Pessoas
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public SexoEnum Sexo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RA { get; set; }
    }
}
