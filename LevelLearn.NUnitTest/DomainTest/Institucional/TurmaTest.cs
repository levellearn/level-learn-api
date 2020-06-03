using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.NUnitTest.Pessoas;
using NUnit.Framework;
using System;
using System.Linq;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class TurmaTest
    {
        // Fields
        private string _nome, _descricao, _nomeDisciplina;

        [SetUp]
        public void Setup()
        {
            _nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            _descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";
            _nomeDisciplina = "Algoritmos";
        }

        [Test]
        public void Cadastrar_TurmaValida_ReturnTrue()
        {
            var turma = CriarTurma();

            bool valido = turma.EstaValido();
            Assert.IsTrue(valido, "Turma deveria ser válida");
        }

        [Test]
        [TestCase("", "Descrição de teste")] // nome inválido
        [TestCase("Tu", "Descrição de teste")] // nome inválido
        [TestCase("Turma XYZ", "")] // descrição inválida
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

            var curso = CursoTest.CriarCursoPadrao();

            var aluno = AlunoTest.CriarAlunoPadrao();
            var professor = ProfessorTest.CriarProfessorPadrao();

            curso.AtribuirPessoa(new PessoaCurso(TiposPessoa.Aluno, aluno.Id, curso.Id));

            var turma = new Turma(_nome, _descricao, _nomeDisciplina, curso.Id, professor.Id);
            turma.AtribuirAluno(new AlunoTurma(aluno.Id, turma.Id));

            return turma;
        }

        public static Turma CriarTurmaPadrao()
        {
            var curso = CursoTest.CriarCursoPadrao();

            var nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            var descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";
            var nomeDisciplina = "ALGORITMOS";

            var professor = curso.Pessoas.First(p => p.Perfil == TiposPessoa.Professor).Pessoa;

            var turma = new Turma(nome, descricao, nomeDisciplina, curso.Id, professor.Id);

            var aluno = curso.Pessoas.First(p => p.Perfil == TiposPessoa.Aluno).Pessoa;

            turma.AtribuirAluno(new AlunoTurma(aluno.Id, turma.Id));

            return turma;
        }

    }
}