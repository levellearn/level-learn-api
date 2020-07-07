using System;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaAtualizaVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Genero { get; set; }      
        public DateTime? DataNascimento { get; set; }
    }
}
