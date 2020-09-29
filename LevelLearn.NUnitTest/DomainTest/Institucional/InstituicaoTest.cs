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
        private string _nome, _descricao, _sigla, _cnpj, _cep, _municipio, _uf;
        private OrganizacaoAcademica _organizacaoAcademica;
        private Rede _rede;
        private CategoriaAdministrativa _categoriaAdministrativa;
        private NivelEnsino _nivelEnsino;

        [SetUp]
        public void Setup()
        {
            _nome = "FATEC Guaratinguetá";
            _descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";
            _sigla = "FATEC GT";
            _cnpj = "62823257000109";
            _cep = "12517010";
            _municipio = "GUARATINGUETA";
            _uf = "SP";
            _organizacaoAcademica = OrganizacaoAcademica.Faculdade;
            _rede = Rede.Publica;
            _categoriaAdministrativa = CategoriaAdministrativa.Estadual;
            _nivelEnsino = NivelEnsino.Superior;
        }

        [Test]
        public void Cadastrar_InstituicaoValida_ReturnTrue()
        {
            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();

            Assert.IsTrue(valido, "Instituição deveria ser válida");
        }

        [Test]
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
            var instituicao = new Instituicao(_nome, _sigla, _descricao, _cnpj, _organizacaoAcademica, _rede, _categoriaAdministrativa, _nivelEnsino, _cep, _municipio, _uf);

            return instituicao;
        }

        public static Instituicao CriarInstituicaoPadrao()
        {
            var nome = "FATEC Guaratinguetá";
            var descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";
            var sigla = "FATEC GT";
            var cnpj = "62823257000109";
            var cep = "12517010";
            var municipio = "GUARATINGUETA";
            var uf = "SP";
            var organizacaoAcademica = OrganizacaoAcademica.Faculdade;
            var rede = Rede.Publica;
            var categoriaAdministrativa = CategoriaAdministrativa.Estadual;
            var nivelEnsino = NivelEnsino.Superior;

            var instituicao = new Instituicao(nome, sigla, descricao, cnpj, organizacaoAcademica, rede, categoriaAdministrativa, nivelEnsino, cep, municipio, uf);

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
