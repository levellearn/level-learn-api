using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Professor : Pessoa
    {
        protected Professor() { }

        public Professor(string nome, string nickName, Email email, CPF cpf, Celular celular, Generos genero, DateTime? dataNascimento)
            : base(nome, nickName, email, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TiposPessoa.Professor;
        }

        public override bool EstaValido()
        {
            base.EstaValido();

            return this.ResultadoValidacao.IsValid;
        }


    }
}
