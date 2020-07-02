using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Professor : Pessoa
    {
        #region Ctors

        protected Professor() { }

        public Professor(string nome, string cpf, string celular, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, new CPF(cpf), new Celular(celular), genero, dataNascimento)
        {
            TipoPessoa = TipoPessoa.Professor;
        }

        public Professor(string nome, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TipoPessoa.Professor;
        }

        #endregion

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
