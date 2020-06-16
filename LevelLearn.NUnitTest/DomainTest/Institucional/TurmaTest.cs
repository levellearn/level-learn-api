using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using NUnit.Framework;
using System;
using System.Linq;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class TurmaTest
    {
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
        [TestCase("", "Descrição de teste", "Algoritmos")] // nome inválido
        [TestCase("Tu", "Descrição de teste", "Algoritmos")] // nome inválido
        [TestCase("Turma XYZ", "", "Algoritmos")] // descrição inválida
        [TestCase("Turma XYZ", "Descrição de teste", "Al")] // nome disciplina inválida
        public void Cadastrar_TurmaValida_ReturnFalse(string nome, string descricao, string nomeDisciplina)
        {
            _nome = nome;
            _nomeDisciplina = nomeDisciplina;
            _descricao = descricao;

            var turma = CriarTurma();
            bool valido = turma.EstaValido();

            Assert.IsFalse(valido, "Turma deveria ser inválida");
        }

        private Turma CriarTurma()
        {
            var turma = new Turma(_nome, _descricao, _nomeDisciplina, Guid.NewGuid(), Guid.NewGuid());

            return turma;
        }

        public static Turma CriarTurmaPadrao()
        {
            var curso = CursoTest.CriarCursoPadrao();

            var nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            var descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";
            var nomeDisciplina = "ALGORITMOS";

            Pessoa professor = curso.Pessoas.First(p => p.Perfil == TipoPessoa.Professor).Pessoa;
            Pessoa aluno = curso.Pessoas.First(p => p.Perfil == TipoPessoa.Aluno).Pessoa;

            var turma = new Turma(nome, descricao, nomeDisciplina, curso.Id, professor.Id);
            var alunoTurma = new AlunoTurma(aluno.Id, turma.Id) { Aluno = aluno, Turma = turma };
            turma.AtribuirAluno(alunoTurma);

            return turma;
        }

    }
}