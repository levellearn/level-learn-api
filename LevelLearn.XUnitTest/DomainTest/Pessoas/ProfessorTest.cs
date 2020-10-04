using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Pessoas
{
    public class ProfessorTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private string _nome, _cpf, _celular;
        private DateTime _dataNascimento;
        private GeneroPessoa _genero;

        public ProfessorTest()
        {
            _nome = "Leandro Guarino";
            _cpf = "881.192.990-35";
            _genero = GeneroPessoa.Masculino;
            _celular = "55(12)98845-8974";
            _dataNascimento = DateTime.Parse("30/12/1988");
        }

        [Fact]
        [Trait("Categoria", "Professores")]
        public void Cadastrar_Professor_ReturnTrue()
        {
            var professor = FakerTest.CriarProfessorFakeValido();

            bool valido = professor.EstaValido();

            Assert.True(valido, "Professor deveria ser válido");
        }

        [Theory]
        [Trait("Categoria", "Professores")]
        [InlineData("123456")]
        [InlineData("(12)98845-8974")]
        public void Cadastrar_Professor_ReturnFalse(string celular)
        {
            _celular = celular;

            var professor = CriarProfessor();

            bool valido = professor.EstaValido();

            Assert.False(valido, "Professor deveria ser inválido");
        }

        [Theory]
        [Trait("Categoria", "Professores")]
        [InlineData("123.456.789-10")]
        [InlineData("111.222.333-44")]
        [InlineData("")]
        public void Cadastrar_ProfessoCPFInvalido_ReturnFalse(string cpf)
        {
            _cpf = cpf;
            var professor = CriarProfessor();

            bool valido = professor.EstaValido();

            Assert.False(valido, "Professor deveria ser inválido");
        }


        private Professor CriarProfessor()
        {
            return new Professor(_nome, new CPF(_cpf), new Celular(_celular),
                _genero, _dataNascimento);
        }

      
    }
}
