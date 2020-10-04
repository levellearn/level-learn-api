using LevelLearn.Domain.Entities.Institucional;
using System;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Institucional
{
    public class CursoTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado
        private string _nome, _sigla, _descricao;

        public CursoTest()
        {
            _nome = "Análise e Desenvolvimento de Sistemas";
            _sigla = "ADS";
            _descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";
        }

        private Curso CriarCurso()
        {
            var curso = new Curso(_nome, _sigla, _descricao, Guid.NewGuid());

            return curso;
        }


        [Fact(DisplayName ="Cadastrar um curso válido")]
        [Trait("Categoria", "Cursos")]
        public void Cadastrar_CursoValido_ReturnTrue()
        {
            var curso = CriarCurso();

            bool valido = curso.EstaValido();
            Assert.True(valido, "Curso deveria ser válido");
        }

        [Theory(DisplayName ="Cadastar curso inválido")]
        [Trait("Categoria", "Cursos")]
        [InlineData("", "ADS", "Descrição de teste")]                                     // nome inválido
        [InlineData("An", "ADS", "Descrição de teste")]                                   // nome inválido
        [InlineData("Análise e Desenvolvimento de Sistemas", "", "Descrição de teste")]   // sigla inválida
        [InlineData("Análise", "ADS", "")]                                                // descrição inválida
        public void Cadastrar_CursoValido_ReturnFalse(string nome, string sigla, string descricao)
        {
            _nome = nome;
            _sigla = sigla;
            _descricao = descricao;

            var curso = CriarCurso();
            bool valido = curso.EstaValido();
            Assert.False(valido, "Curso deveria ser inválido");
        }



    }
}
