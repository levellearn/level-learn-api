using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.NUnitTest.Pessoas;
using NUnit.Framework;
using System;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class CursoTest
    {
        #region Fields
        private string _nome, _sigla, _descricao;
        #endregion

        [SetUp]
        public void Setup()
        {
            _nome = "Análise e Desenvolvimento de Sistemas";
            _sigla = "ADS";
            _descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, " +
                "implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, " +
                "com ferramentas computacionais, equipamentos de informática e metodologia de projetos na " +
                "produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias " +
                "de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança " +
                "de programas computacionais são fundamentais à atuação desse profissional.";
        }

        [Test]
        public void Cadastrar_InstituicaoValida_ReturnTrue()
        {
            var curso = CriarCurso();

            bool valido = curso.EstaValido();
            Assert.IsTrue(valido, "Curso deveria ser válido");
        }

        [Test]
        [TestCase("", "ADS", "Descrição de teste")]
        [TestCase("Análise e Desenvolvimento de Sistemas", "", "Descrição de teste")]
        [TestCase("Análise", "ADS", "")]
        [TestCase("An", "ADS", "Descrição de teste")]
        public void Cadastrar_InstituicaoValida_ReturnFalse(string nome, string sigla, string descricao)
        {
            _nome = nome;
            _sigla = sigla;
            _descricao = descricao;

            var curso = CriarCurso();
            bool valido = curso.EstaValido();
            Assert.IsFalse(valido, "Curso deveria ser inválido");
        }

        private Curso CriarCurso()
        {
            var instituicao = InstituicaoTest.CriarInstituicaoPadrao();

            var curso = new Curso(_nome, _sigla, _descricao, instituicao.Id);

            instituicao.AtribuirCurso(curso);

            var aluno = AlunoTest.CriarAlunoPadrao();
            var professor = ProfessorTest.CriarProfessorPadrao();

            var pessoaCurso = new PessoaCurso(TiposPessoa.Aluno, aluno.Id, curso.Id);

            curso.AtribuirPessoa(pessoaCurso);

            return curso;
        }

        public static Curso CriarCursoPadrao(Guid instituicaoId)
        {
            var nome = "Análise e Desenvolvimento de Sistemas";
            var sigla = "ADS";
            var descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, " +
                "implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, " +
                "com ferramentas computacionais, equipamentos de informática e metodologia de projetos na " +
                "produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias " +
                "de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança " +
                "de programas computacionais são fundamentais à atuação desse profissional.";

            return new Curso(nome, sigla, descricao, instituicaoId);
        }

    }
}