using LevelLearn.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Pessoas
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public SexoEnum Sexo { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public string Imagem { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RA { get; set; }

        public List<PessoaInstituicao> Instituicoes { get; set; } = new List<PessoaInstituicao>();
    }
}
