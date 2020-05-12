using LevelLearn.ViewModel.Enums;
using System;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaVM
    {
        public Guid Id { get; set; }
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Cpf { get; protected set; }
        public string Celular { get; protected set; }
        public GenerosVM Genero { get; protected set; }
        public TiposPessoaVM TipoPessoa { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }

        //public virtual ICollection<PessoaInstituicao> Instituicoes { get; protected set; }
        //public virtual ICollection<PessoaCurso> Cursos { get; protected set; }
        //public virtual ICollection<Turma> Turmas { get; protected set; }
    }
}
