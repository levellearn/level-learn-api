using System;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaDetalheVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Genero { get; set; }
        public string TipoPessoa { get; set; }
        public DateTime? DataNascimento { get; set; }

        //public ICollection<PessoaInstituicao> Instituicoes { get; set; }
        //public ICollection<PessoaCurso> Cursos { get; set; }
        //public ICollection<Turma> Turmas { get; set; }
    }
}
