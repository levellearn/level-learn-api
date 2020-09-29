using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Admin : Pessoa
    {
        protected Admin() { }

        public Admin(string nome, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
            : base(nome, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TipoPessoa.Admin;
        }

    }
}
