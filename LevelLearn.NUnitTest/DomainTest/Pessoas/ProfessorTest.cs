using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using NUnit.Framework;
using System;

namespace LevelLearn.NUnitTest.Pessoas
{
    [TestFixture]
    class ProfessorTest
    {
        private string _nome, _cpf, _celular;
        private DateTime _dataNascimento;
        private GeneroPessoa _genero;

        [SetUp]
        public void Setup()
        {
            _nome = "Leandro Guarino";
            _cpf = "881.192.990-35";
            _genero = GeneroPessoa.Masculino;
            _celular = "55(12)98845-8974";
            _dataNascimento = DateTime.Parse("30/12/1988");
        }

        [Test]
        public void Cadastrar_Professor_ReturnTrue()
        {
            var professor = CriarProfessor();

            bool valido = professor.EstaValido();

            Assert.IsTrue(valido, "Professor deveria ser válido");
        }

        [Test]
        [TestCase("123456")]
        [TestCase("(12)98845-8974")]
        public void Cadastrar_Professor_ReturnFalse(string celular)
        {
            _celular = celular;

            var professor = CriarProfessor();

            bool valido = professor.EstaValido();

            Assert.IsFalse(valido, "Professor deveria ser inválido");
        }

        [Test]
        [TestCase("123.456.789-10")]
        [TestCase("111.222.333-44")]
        [TestCase("")]
        public void Cadastrar_ProfessoCPFInvalido_ReturnFalse(string cpf)
        {
            _cpf = cpf;
            var professor = CriarProfessor();

            bool valido = professor.EstaValido();

            Assert.IsFalse(valido, "Professor deveria ser inválido");
        }


        private Professor CriarProfessor()
        {
            return new Professor(_nome, new CPF(_cpf), new Celular(_celular),
                _genero, _dataNascimento);
        }

        public static Professor CriarProfessorPadrao()
        {
            var nome = "Leandro Guarino";
            var cpf = "881.192.990-35";
            var genero = GeneroPessoa.Masculino;
            var celular = "55(12)98845-8974";
            var dataNascimento = DateTime.Parse("30/12/1988");

            return new Professor(nome, new CPF(cpf), new Celular(celular),
                genero, dataNascimento);
        }

    }
}
