using LevelLearn.Domain.Entities.Pessoas;
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
        // Fields
        private string _nome, _nickName, _email, _cpf, _celular, _imagemUrl;
        private DateTime _dataNascimento;
        private Generos _genero;

        [SetUp]
        public void Setup()
        {
            _nome = "Leandro Guarino";
            _nickName = "le_guarino";
            _email = "le.guarino@mail.com";
            _cpf = "881.192.990-35";
            _genero = Generos.Masculino;
            _celular = "(12)98845-8974";
            _imagemUrl = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";
            _dataNascimento = DateTime.Parse("30/12/1988");
        }

        [Test]
        public void Cadastrar_ProfessorValido_ReturnTrue()
        {
            var professor = CriarProfessor();
            bool valido = professor.EstaValido();
            Assert.IsTrue(valido, "Professor deveria ser válido");
        }

        [Test]
        [TestCase("le.guarino@mail", "(12)98845-8974")] // email inválido
        [TestCase("le.guarino@mail.com", "123456")] // celular inválido
        public void Cadastrar_ProfessorValido_ReturnFalse(string email, string celular)
        {
            _email = email;
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
            return new Professor(_nome, _nickName, new Email(_email), new CPF(_cpf), new Celular(_celular),
                _genero, _imagemUrl, _dataNascimento);
        }

        public static Professor CriarProfessorPadrao()
        {
            var nome = "Leandro Guarino";
            var nickName = "le_guarino";
            var email = "le.guarino@mail.com";
            var cpf = "881.192.990-35";
            var genero = Generos.Masculino;
            var celular = "(12)98845-8974";
            var imagemUrl = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";
            var dataNascimento = DateTime.Parse("30/12/1988");

            return new Professor(nome, nickName, new Email(email), new CPF(cpf), new Celular(celular),
                genero, imagemUrl, dataNascimento);
        }

    }
}
