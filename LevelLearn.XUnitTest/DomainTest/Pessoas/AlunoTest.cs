using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Pessoas
{
    public class AlunoTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private string _nome, _cpf, _ra, _celular;
        private DateTime _dataNascimento;
        private GeneroPessoa _genero;

        public AlunoTest()
        {
            _nome = "Felipe Ayres";
            _cpf = "881.192.990-35";
            _genero = GeneroPessoa.Masculino;
            _celular = "55(12)98845-7832";
            _ra = "f1310435";
            _dataNascimento = DateTime.Parse("26/10/1993");
        }

        [Fact]
        [Trait("Categoria", "Alunos")]
        public void Cadastrar_AlunoValido_ReturnTrue()
        {
            Aluno aluno = FakerTest.CriarAlunoFakeValido();

            bool valido = aluno.EstaValido();

            Assert.True(valido, "Aluno deveria ser válido");
        }

        [Theory]
        [Trait("Categoria", "Alunos")]
        [InlineData("123.456.789-10", "29/10/1990")] // CPF inválido
        [InlineData("881.192.990-35", "29/10/5000")] // Data inválida
        public void Cadastrar_AlunoValido_ReturnFalse(string cpf, string dataNascimento)
        {
            _cpf = cpf;
            _dataNascimento = DateTime.Parse(dataNascimento);

            var aluno = CriarAluno();

            bool valido = aluno.EstaValido();

            Assert.False(valido, "Aluno deveria ser inválido");
        }

        [Theory]
        [Trait("Categoria", "Alunos")]
        [InlineData("William Henry Gates III")]
        [InlineData("Shaquille O'Neal")]
        [InlineData("Steven P. Jobs")]
        [InlineData("Joseph Louis Gay-Lussac")]
        [InlineData("Francisco O' Lourenço")]
        [InlineData("Ma Long Np")]
        public void Aluno_NomeCompleto_ReturnTrue(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.Propriedade == nameof(Pessoa.Nome));

            Assert.True(condition, "Aluno deveria ter nome completo");
        }

        [Theory]
        [Trait("Categoria", "Alunos")]
        [InlineData("F lipe")]
        [InlineData("Felipe A")]
        [InlineData("F Ayres")]
        [InlineData("Felipe")]
        public void Aluno_NomeCompleto_ReturnFalse(string nome)
        {
            _nome = nome;
            var aluno = CriarAluno();

            aluno.EstaValido();
            var erros = aluno.DadosInvalidos().ToList();
            var condition = !erros.Exists(e => e.Propriedade == nameof(Pessoa.Nome));

            Assert.False(condition, "Aluno deveria ter nome incompleto");
        }

        [Fact]
        [Trait("Categoria", "Alunos")]
        public void Cadastrar_AlunoSemCPF_ReturnTrue()
        {
            _cpf = null;
            var aluno = CriarAluno();

            bool valido = aluno.EstaValido();

            Assert.True(valido, "Aluno deveria ser válido");
        }

        private Aluno CriarAluno()
        {
            return new Aluno(_nome, new CPF(_cpf), new Celular(_celular), _ra,
                _genero, _dataNascimento);
        }
      
    }
}
