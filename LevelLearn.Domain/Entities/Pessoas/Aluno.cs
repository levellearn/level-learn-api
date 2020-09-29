using FluentValidation.Results;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Aluno : Pessoa
    {
        #region Ctors

        protected Aluno() { }

        public Aluno(string nome, string cpf, string celular, string ra, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, new CPF(cpf), new Celular(celular), genero, dataNascimento)
        {
            RA = ra.RemoveExtraSpaces();
            TipoPessoa = TipoPessoa.Aluno;
        }

        public Aluno(string nome, CPF cpf, Celular celular, string ra, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, cpf, celular, genero, dataNascimento)
        {
            RA = ra.RemoveExtraSpaces();
            TipoPessoa = TipoPessoa.Aluno;
        }

        #endregion

        // Props
        public string RA { get; private set; }

        // Methods
        public override bool EstaValido()
        {
            base.EstaValido();

            var alunoValidator = new AlunoValidator();
            ValidationResult alunoValidationResult = alunoValidator.Validate(this);

            if (!alunoValidationResult.IsValid)
                ResultadoValidacao.AddErrors(alunoValidationResult);

            return ResultadoValidacao.IsValid;
        }

    }
}
