﻿using LevelLearn.ViewModel.Enums;
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
        public GenerosVM Genero { get; set; }
        public TiposPessoaVM TipoPessoa { get; set; }
        public DateTime? DataNascimento { get; set; }        
    }
}