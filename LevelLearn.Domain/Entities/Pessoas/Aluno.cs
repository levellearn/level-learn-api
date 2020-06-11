using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Aluno : Pessoa
    {
        // Ctors
        protected Aluno() { }

        public Aluno(string nome, Email email, CPF cpf, Celular celular, string ra, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, email, cpf, celular, genero, dataNascimento)
        {
            RA = ra.RemoveExtraSpaces();
            TipoPessoa = TipoPessoa.Aluno;
        }

        // Props
        public string RA { get; private set; }

        // Methods
        public override bool EstaValido()
        {
            base.EstaValido();

            var alunoValidator = new AlunoValidator();
            var alunoValidationResult = alunoValidator.Validate(this);

            if (!alunoValidationResult.IsValid)
                this.ResultadoValidacao.AddErrors(alunoValidationResult);

            return this.ResultadoValidacao.IsValid;
        }

    }
}
