using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public GeneroPessoa Genero { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
