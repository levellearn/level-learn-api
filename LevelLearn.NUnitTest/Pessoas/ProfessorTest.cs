﻿using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace LevelLearn.NUnitTest.Pessoas
{
    [TestFixture]
    class ProfessorTest
    {
        #region Fields
        private string _nome, _userName, _email, _cpf, _celular, _imagemUrl;
        private DateTime _dataNascimento;
        private Generos _genero;
        #endregion

        [SetUp]
        public void Setup()
        {
            _nome = "Leandro Guarino";
            _userName = "le_guarino";
            _email = "le.guarino@mail.com";
            _cpf = "881.192.990-35";
            _genero = Generos.Masculino;
            _celular = "(12)98845-8974";
            _imagemUrl = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";
            _dataNascimento = DateTime.Parse("30/12/1988");
        }

        //[Test]
        //public void Cadastrar_AlunoValido_ReturnTrue()
        //{
        //    var aluno = CriarAluno();
        //    bool valido = aluno.EstaValido();
        //    Assert.IsTrue(valido, "Aluno deveria ser válido");
        //}

        //[Test]
        //[TestCase("123.456.789-10", "29/10/1993")]
        //[TestCase("881.192.990-35", "")]
        //public void Cadastrar_AlunoValido_ReturnFalse(string cpf, string dataNascimento)
        //{
        //    _cpf = cpf;
        //    _dataNascimento = string.IsNullOrEmpty(dataNascimento) ? DateTime.Now.Date : DateTime.Parse(dataNascimento);

        //    var aluno = CriarAluno();
        //    bool valido = aluno.EstaValido();
        //    Assert.IsFalse(valido, "Aluno deveria ser inválido");
        //}


        //[Test]
        //[TestCase("William Henry Gates III")]
        //[TestCase("Shaquille O'Neal")]
        //[TestCase("Steven P. Jobs")]
        //[TestCase("Joseph Louis Gay-Lussac")]
        //public void Aluno_NomeCompleto_ReturnTrue(string nome)
        //{
        //    _nome = nome;
        //    var aluno = CriarAluno();
        //    aluno.EstaValido();
        //    var erros = aluno.DadosInvalidos().ToList();
        //    var condition = !erros.Exists(e => e.ErrorMessage == "O Nome precisa de um sobrenome");

        //    Assert.IsTrue(condition, "Aluno deveria ter nome completo");
        //}

        //[Test]
        //[TestCase("Fe lipe")]
        //[TestCase("Felipe A")]
        //[TestCase("F. Ayres")]
        //[TestCase("Felipe")]
        //public void Aluno_NomeCompleto_ReturnFalse(string nome)
        //{
        //    _nome = nome;
        //    var aluno = CriarAluno();
        //    aluno.EstaValido();
        //    var erros = aluno.DadosInvalidos().ToList();
        //    var condition = !erros.Exists(e => e.ErrorMessage == "O Nome precisa de um sobrenome");

        //    Assert.IsFalse(condition, "Aluno deveria ter nome imcompleto");
        //}


        //[Test]
        //[TestCase("billGates3")]
        //[TestCase("shaq_O-Neal")]
        //[TestCase("steven.jobs")]
        //public void Aluno_UsernameValido_ReturnTrue(string userName)
        //{
        //    _userName = userName;
        //    var aluno = CriarAluno();
        //    aluno.EstaValido();
        //    var erros = aluno.DadosInvalidos().ToList();
        //    bool valido = !erros.Exists(e => e.PropertyName == "UserName");

        //    Assert.IsTrue(valido, "Aluno deveria ter UserName válido");
        //}

        //[Test]
        //[TestCase("bill@Gates3")]
        //[TestCase("shaq$Neal")]
        //[TestCase("#stevenjobs")]
        //public void Aluno_UsernameValido_ReturnFalse(string userName)
        //{
        //    _userName = userName;
        //    var aluno = CriarProfessor();
        //    aluno.EstaValido();
        //    var erros = aluno.DadosInvalidos().ToList();
        //    bool valido = !erros.Exists(e => e.PropertyName == "UserName");

        //    Assert.IsFalse(valido, "Aluno deveria ter UserName inválido");
        //}


        //private Aluno CriarProfessor()
        //{
        //    return new Professor(_nome, _userName, new Email(_email), new CPF(_cpf), new Celular(_celular),
        //        _genero, _imagemUrl, _dataNascimento);
        //}

        public static Professor CriarProfessorPadrao()
        {
            var nome = "Leandro Guarino";
            var userName = "le_guarino";
            var email = "le.guarino@mail.com";
            var cpf = "881.192.990-35";
            var genero = Generos.Masculino;
            var celular = "(12)98845-8974";
            var imagemUrl = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";
            var dataNascimento = DateTime.Parse("30/12/1988");

            return new Professor(nome, userName, new Email(email), new CPF(cpf), new Celular(celular),
                genero, imagemUrl, dataNascimento);
        }

    }
}
