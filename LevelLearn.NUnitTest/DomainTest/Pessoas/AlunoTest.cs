using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Linq;
//https://docs.microsoft.com/pt-br/dotnet/core/testing/unit-testing-with-nunit

namespace LevelLearn.NUnitTest.Pessoas
{
    [TestFixture]
    class AlunoTest
    {
        private string _nome, _email, _cpf, _ra, _celular;
        private DateTime _dataNascimento;
        private GeneroPessoa _genero;

        [SetUp]
        public void Setup()
        {
            _nome = "Felipe Ayres";
            _email = "felipe.ayres@mail.com";
            _cpf = "881.192.990-35";
            _genero = GeneroPessoa.Masculino;
            _celular = "55(12)98845-7832";
            _ra = "f1310435";
            _dataNascimento = DateTime.Parse("26/10/1993");
        }

        [Test]
        public void Cadastrar_AlunoValido_ReturnTrue()
        {
            Aluno aluno = CriarAluno();

            bool valido = aluno.EstaValido();

            Assert.IsTrue(valido, "Aluno deveria ser válido");
        }

        [Test]
        [TestCase("123.456.789-10", "29/10/1990")] // CPF inválido
        [TestCase("881.192.990-35", "29/10/5000")] // Data inválida
        public void Cadastrar_AlunoValido_ReturnFalse(string cpf, string dataNascimento)
        {
            _cpf = cpf;
            _dataNascimento = DateTime.Parse(dataNascimento);

            var aluno = CriarAluno();

            bool valido = aluno.EstaValido();

            Assert.IsFalse(valido, "Aluno deveria ser inválido");
        }

        [Test]
        [TestCase("William Henry Gates III")]
        [TestCase("Shaquille O'Neal")]
        [TestCase("Steven P. Jobs")]
        [TestCase("Joseph Louis Gay-Lussac")]
        [TestCase("Francisco O' Lourenço")]
        [TestCase("Ma Long Np")]
        public void Aluno_NomeCompleto_ReturnTrue(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.Propriedade == nameof(Pessoa.Nome));

            Assert.IsTrue(condition, "Aluno deveria ter nome completo");
        }

        [Test]
        [TestCase("F lipe")]
        [TestCase("Felipe A")]
        [TestCase("F Ayres")]
        [TestCase("Felipe")]
        public void Aluno_NomeCompleto_ReturnFalse(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.Propriedade == nameof(Pessoa.Nome));

            Assert.IsFalse(condition, "Aluno deveria ter nome incompleto");
        }

        [Test]
        public void Cadastrar_AlunoSemCPF_ReturnTrue()
        {
            _cpf = null;
            var aluno = CriarAluno();

            bool valido = aluno.EstaValido();

            Assert.IsTrue(valido, "Aluno deveria ser válido");
        }

        private Aluno CriarAluno()
        {
            return new Aluno(_nome, new Email(_email), new CPF(_cpf), new Celular(_celular), _ra,
                _genero, _dataNascimento);
        }

        public static Aluno CriarAlunoPadrao()
        {
            var nome = "Felipe Ayres";
            var email = "felipe.ayres@mail.com";
            var cpf = "881.192.990-35";
            var genero = GeneroPessoa.Masculino;
            var celular = "(12)98845-7832";
            var ra = "f1310435";
            var dataNascimento = DateTime.Parse("26/10/1993");

            return new Aluno(nome, new Email(email), new CPF(cpf), new Celular(celular), ra,
                genero, dataNascimento);
        }

    }
}
