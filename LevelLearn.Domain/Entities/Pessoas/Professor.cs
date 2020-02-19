using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Professor : Pessoa
    {
        protected Professor() { }

        public Professor(string nome, string userName, Email email, CPF cpf, Celular celular, Generos genero, string imagemUrl, DateTime? dataNascimento)
            : base(nome, userName, email, cpf, celular, genero, imagemUrl, dataNascimento)
        {
            TipoPessoa = TiposPessoa.Professor;
        }

    }
}
