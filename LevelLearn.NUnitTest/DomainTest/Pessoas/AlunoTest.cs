using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Usuarios;
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
        // Fields
        private string _nome, _nickName, _email, _cpf, _ra, _celular;
        private DateTime _dataNascimento;
        private Generos _genero;
        private readonly IValidatorApp<Aluno> _validator = new AlunoValidator();

        [SetUp]
        public void Setup()
        {
            _nome = "Felipe Ayres";
            _nickName = "felipe_ayres";
            _email = "felipe.ayres@mail.com";
            _cpf = "881.192.990-35";
            _genero = Generos.Masculino;
            _celular = "(12)98845-7832";
            _ra = "f1310513";
            _dataNascimento = DateTime.Parse("26/10/1993");
        }

        [Test]
        public void Cadastrar_AlunoValido_ReturnTrue()
        {
            var aluno = CriarAluno();

            _validator.Validar(aluno);
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

            _validator.Validar(aluno);
            bool valido = aluno.EstaValido();

            Assert.IsFalse(valido, "Aluno deveria ser inválido");
        }


        [Test]
        [TestCase("William Henry Gates III")]
        [TestCase("Shaquille O'Neal")]
        [TestCase("Steven P. Jobs")]
        [TestCase("Joseph Louis Gay-Lussac")]
        [TestCase("Francisco O' Lourenço")]
        public void Aluno_NomeCompleto_ReturnTrue(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            _validator.Validar(aluno);
            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.PropertyName == nameof(Pessoa.Nome));

            Assert.IsTrue(condition, "Aluno deveria ter nome completo");
        }

        [Test]
        [TestCase("Fe lipe")]
        [TestCase("Felipe A")]
        [TestCase("F. Ayres")]
        [TestCase("Felipe")]
        public void Aluno_NomeCompleto_ReturnFalse(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            _validator.Validar(aluno);
            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.PropertyName == nameof(Pessoa.Nome));

            Assert.IsFalse(condition, "Aluno deveria ter nome imcompleto");
        }


        [Test]
        [TestCase("billGates3")]
        [TestCase("shaq_O-Neal")]
        [TestCase("steven.jobs")]
        public void Aluno_NicknameValido_ReturnTrue(string userName)
        {
            _nickName = userName;
            var aluno = CriarAluno();

            _validator.Validar(aluno);
            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.PropertyName == nameof(Pessoa.NickName));

            Assert.IsTrue(valido, "Aluno deveria ter NickName válido");
        }

        [Test]
        [TestCase("bill@Gates3")]
        [TestCase("shaq$Neal")]
        [TestCase("#stevenjobs")]
        public void Aluno_NicknameValido_ReturnFalse(string userName)
        {
            _nickName = userName;
            var aluno = CriarAluno();

            _validator.Validar(aluno);
            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.PropertyName == nameof(Pessoa.NickName));

            Assert.IsFalse(valido, "Aluno deveria ter NickName inválido");
        }

        [Test]
        public void Cadastrar_AlunoSemCPF_ReturnTrue()
        {
            _cpf = null;
            var aluno = CriarAluno();

            _validator.Validar(aluno);
            bool valido = aluno.EstaValido();

            Assert.IsTrue(valido, "Aluno deveria ser válido");
        }

        private Aluno CriarAluno()
        {
            return new Aluno(_nome, _nickName, new Email(_email), new CPF(_cpf), new Celular(_celular), _ra,
                _genero, _dataNascimento);
        }

        public static Aluno CriarAlunoPadrao()
        {
            var nome = "Felipe Ayres";
            var nickName = "felipe_ayres";
            var email = "felipe.ayres@mail.com";
            var cpf = "881.192.990-35";
            var genero = Generos.Masculino;
            var celular = "(12)98845-7832";
            var ra = "f1310513";
            var dataNascimento = DateTime.Parse("26/10/1993");

            return new Aluno(nome, nickName, new Email(email), new CPF(cpf), new Celular(celular), ra,
                genero, dataNascimento);
        }

    }
}
