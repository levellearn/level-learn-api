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


        /// <summary>
        /// Atualiza os dados da instância atual
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="celular"></param>
        /// <param name="genero"></param>
        /// <param name="dataNascimento"></param>
        /// <returns>Retorna true se ocorreu mudança ou falso se não ocorreu mudança</returns>
        public bool Atualizar(string nome, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
        {
            bool modificado = Nome.Equals(nome) || Cpf.Equals(cpf) || Celular.Equals(celular) 
                || Genero.Equals(genero) || DataNascimento.Equals(dataNascimento);

            Nome = nome.RemoveExtraSpaces();
            Cpf = cpf;
            Celular = celular;
            Genero = genero;
            DataNascimento = dataNascimento;

            AtribuirNomePesquisa();

            return modificado;
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
