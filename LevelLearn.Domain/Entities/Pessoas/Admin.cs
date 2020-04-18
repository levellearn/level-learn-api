using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class Admin : Pessoa
    {
        protected Admin() { }

        public Admin(string nome, string nickName, Email email, CPF cpf, Celular celular, Generos genero, DateTime? dataNascimento)
            : base(nome, nickName, email, cpf, celular, genero, dataNascimento)
        {
            TipoPessoa = TiposPessoa.Admin;
        }

    }
}
