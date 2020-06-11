using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Admin : Pessoa
    {
        protected Admin() { }

        public Admin(string nome, Email email, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, email, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TipoPessoa.Admin;
        }

    }
}
