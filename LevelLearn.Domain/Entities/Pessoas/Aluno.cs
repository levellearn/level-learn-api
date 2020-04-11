﻿using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Pessoas;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Aluno : Pessoa
    {
        // Ctors
        protected Aluno() { }

        public Aluno(string nome, string nickName, Email email, CPF cpf, Celular celular, string ra, Generos genero, string imagemUrl, DateTime? dataNascimento)
            : base(nome, nickName, email, cpf, celular, genero, imagemUrl, dataNascimento)
        {
            RA = ra.RemoveExtraSpaces();
            TipoPessoa = TiposPessoa.Aluno;
        }

        // Props

        public string RA { get; private set; }

        // Methods

        public override bool EstaValido()
        {
            base.EstaValido();          

            return this.ResultadoValidacao.IsValid;
        }

    }
}
