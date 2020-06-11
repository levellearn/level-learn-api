using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Professor : Pessoa
    {
        protected Professor() { }

        public Professor(string nome, Email email, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, email, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TipoPessoa.Professor;
        }

        public override bool EstaValido()
        {
            base.EstaValido();

            var professorValidator = new ProfessorValidator();
            var professorValidationResult = professorValidator.Validate(this);

            if (!professorValidationResult.IsValid)
                this.ResultadoValidacao.AddErrors(professorValidationResult);

            return this.ResultadoValidacao.IsValid;
        }


    }
}
