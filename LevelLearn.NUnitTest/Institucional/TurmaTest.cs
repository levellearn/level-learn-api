using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.NUnitTest.Pessoas;
using NUnit.Framework;
using System;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class TurmaTest
    {
        #region Fields
        private string _nome, _descricao;
        #endregion

        [SetUp]
        public void Setup()
        {
            _nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            _descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso" +
                " de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de " +
                "algoritmos e programas de computador.";
        }

        [Test]
        public void Cadastrar_TurmaValida_ReturnTrue()
        {
            var turma = CriarTurma();

            bool valido = turma.EstaValido();
            Assert.IsTrue(valido, "Turma deveria ser válida");
        }

        [Test]
        [TestCase("", "Descrição de teste")]
        [TestCase("Turma XYZ", "")]
        [TestCase("Tu", "Descrição de teste")]
        public void Cadastrar_TurmaValida_ReturnFalse(string nome, string descricao)
        {
            _nome = nome;
            _descricao = descricao;

            var turma = CriarTurma();
            bool valido = turma.EstaValido();
            Assert.IsFalse(valido, "Turma deveria ser inválida");
        }

        private Turma CriarTurma()
        {
            var instituicao = InstituicaoTest.CriarInstituicaoPadrao();

            var curso = CursoTest.CriarCursoPadrao(instituicao.Id);

            instituicao.AtribuirCurso(curso);

            var aluno = AlunoTest.CriarAlunoPadrao();
            var professor = ProfessorTest.CriarProfessorPadrao();

            curso.AtribuirPessoa(new PessoaCurso(TiposPessoa.Aluno, aluno.Id, curso.Id));

            var turma = new Turma(_nome, _descricao, curso.Id, professor.Id);
            turma.AtribuirAluno(new AlunoTurma(aluno.Id, turma.Id));

            return turma;
        }

        public static Turma CriarTurmaPadrao(Guid cursoId, Guid professorId)
        {
            var nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            var descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso" +
                " de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de " +
                "algoritmos e programas de computador.";

            return new Turma(nome, descricao, cursoId, professorId);
        }

    }
}