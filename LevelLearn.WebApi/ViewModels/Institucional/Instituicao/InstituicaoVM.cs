using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoVM
    {
        [Required(ErrorMessage = "Campo Id é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        public ICollection<CursoVM> Cursos { get; set; }
        //public ICollection<PessoaInstituicao> Pessoas { get; set; }
    }
}
