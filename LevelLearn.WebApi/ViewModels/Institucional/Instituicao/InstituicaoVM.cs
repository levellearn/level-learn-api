using System;
using System.Collections.Generic;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<CursoVM> Cursos { get; set; } = new List<CursoVM>();
        //public ICollection<PessoaInstituicao> Pessoas { get; set; }
    }
}
