using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.NUnitTest.Pessoas;
using NUnit.Framework;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class InstituicaoTest
    {
        // Fields
        private string _nome, _descricao;

        [SetUp]
        public void Setup()
        {
            _nome = "FATEC Guaratinguetá";
            _descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";
        }

        [Test]
        public void Cadastrar_InstituicaoValida_ReturnTrue()
        {
            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();

            Assert.IsTrue(valido, "Instituição deveria ser válida");
        }

        [Test]
        [TestCase("UniFatea", "")] // descrição inválida
        [TestCase("Un", "")] // nome e descrição inválida
        [TestCase("", "Descrição de teste")] // nome inválido
        public void Cadastrar_InstituicaoValida_ReturnFalse(string nome, string descricao)
        {
            _nome = nome;
            _descricao = descricao;

            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();

            Assert.IsFalse(valido, "Instituição deveria ser inválida");
        }

        private Instituicao CriarInstituicao()
        {
            var instituicao = new Instituicao(_nome, _descricao);

            var aluno = AlunoTest.CriarAlunoPadrao();
            var professor = ProfessorTest.CriarProfessorPadrao();

            var professorAdminInstituicao = new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, professor.Id, instituicao.Id);
            var alunoInstituicao = new PessoaInstituicao(PerfilInstituicao.Aluno, aluno.Id, instituicao.Id);

            instituicao.AtribuirPessoa(professorAdminInstituicao);
            instituicao.AtribuirPessoa(alunoInstituicao);

            return instituicao;
        }

        public static Instituicao CriarInstituicaoPadrao()
        {
            var nome = "FATEC Guaratinguetá";
            var descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";

            var instituicao = new Instituicao(nome, descricao);

            var aluno = AlunoTest.CriarAlunoPadrao();
            var professor = ProfessorTest.CriarProfessorPadrao();

            var professorAdminInstituicao = new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, professor.Id, instituicao.Id)
            {
                Pessoa = professor,
                Instituicao = instituicao
            };
            var alunoInstituicao = new PessoaInstituicao(PerfilInstituicao.Aluno, aluno.Id, instituicao.Id)
            {
                Pessoa = aluno,
                Instituicao = instituicao
            };

            instituicao.AtribuirPessoa(professorAdminInstituicao);
            instituicao.AtribuirPessoa(alunoInstituicao);


            return instituicao;
        }


    }
}
