﻿using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class CadastrarInstituicaoVM
    {
        //[Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

    }
}