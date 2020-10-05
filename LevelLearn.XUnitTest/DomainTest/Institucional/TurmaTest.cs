using LevelLearn.Domain.Entities.Institucional;
using System;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Institucional
{
    public class TurmaTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private string _nome, _descricao, _nomeDisciplina;
        public TurmaTest()
        {
            _nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            _descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";
            _nomeDisciplina = "Algoritmos";
        }

        [Trait("Categoria", "Turmas")]
        [Fact]
        public void Cadastrar_TurmaValida_ReturnTrue()
        {
            var turma = CriarTurma();

            bool valido = turma.EstaValido();
            Assert.True(valido, "Turma deveria ser válida");
        }

        [Theory]
        [Trait("Categoria", "Turmas")]
        [InlineData("", "Descrição de teste", "Algoritmos")] // nome inválido
        [InlineData("Tu", "Descrição de teste", "Algoritmos")] // nome inválido
        [InlineData("Turma XYZ", "", "Algoritmos")] // descrição inválida
        [InlineData("Turma XYZ", "Descrição de teste", "Al")] // nome disciplina inválida
        public void Cadastrar_TurmaValida_ReturnFalse(string nome, string descricao, string nomeDisciplina)
        {
            _nome = nome;
            _nomeDisciplina = nomeDisciplina;
            _descricao = descricao;

            var turma = CriarTurma();
            bool valido = turma.EstaValido();

            Assert.False(valido, "Turma deveria ser inválida");
        }

        private Turma CriarTurma()
        {
            var turma = new Turma(_nome, _descricao, _nomeDisciplina, Guid.NewGuid(), Guid.NewGuid());

            return turma;
        }

    }
}
