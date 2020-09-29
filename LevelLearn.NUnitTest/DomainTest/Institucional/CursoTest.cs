using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using NUnit.Framework;
using System;
using System.Linq;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class CursoTest
    {
        private string _nome, _sigla, _descricao;

        [SetUp]
        public void Setup()
        {
            _nome = "Análise e Desenvolvimento de Sistemas";
            _sigla = "ADS";
            _descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";
        }

        [Test]
        public void Cadastrar_CursoValido_ReturnTrue()
        {
            var curso = CriarCurso();

            bool valido = curso.EstaValido();
            Assert.IsTrue(valido, "Curso deveria ser válido");
        }

        [Test]
        [TestCase("", "ADS", "Descrição de teste")]                                     // nome inválido
        [TestCase("An", "ADS", "Descrição de teste")]                                   // nome inválido
        [TestCase("Análise e Desenvolvimento de Sistemas", "", "Descrição de teste")]   // sigla inválida
        [TestCase("Análise", "ADS", "")]                                                // descrição inválida
        public void Cadastrar_CursoValido_ReturnFalse(string nome, string sigla, string descricao)
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
            var curso = new Curso(_nome, _sigla, _descricao, Guid.NewGuid());
         
            return curso;
        }

        public static Curso CriarCursoPadrao()
        {
            var instituicao = InstituicaoTest.CriarInstituicaoPadrao();

            var nome = "Análise e Desenvolvimento de Sistemas";
            var sigla = "ADS";
            var descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";

            var curso = new Curso(nome, sigla, descricao, instituicao.Id);

            var professor = instituicao.Pessoas.First(p => p.Perfil == PerfilInstituicao.ProfessorAdmin).Pessoa;
            var aluno = instituicao.Pessoas.First(p => p.Perfil == PerfilInstituicao.Aluno).Pessoa;

            var professorCurso = new PessoaCurso(TipoPessoa.Professor, professor.Id, curso.Id)
            {
                Pessoa = professor,
                Curso = curso
            };             
            var alunoCurso = new PessoaCurso(TipoPessoa.Aluno, aluno.Id, curso.Id)
            {
                Pessoa = professor,
                Curso = curso
            };

            curso.AtribuirPessoa(professorCurso);
            curso.AtribuirPessoa(alunoCurso);

            instituicao.AtribuirCurso(curso);

            return curso;
        }

    }
}