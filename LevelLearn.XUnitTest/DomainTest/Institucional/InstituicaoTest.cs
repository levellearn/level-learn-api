using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Institucional
{
    public class InstituicaoTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private string _nome, _descricao, _sigla, _cnpj, _cep, _municipio, _uf;
        private OrganizacaoAcademica _organizacaoAcademica;
        private Rede _rede;
        private CategoriaAdministrativa _categoriaAdministrativa;
        private NivelEnsino _nivelEnsino;

        public InstituicaoTest()
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

        [Fact]
        [Trait("Categoria", "Instituicoes")]
        public void Cadastrar_InstituicaoValida_ReturnTrue()
        {
            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();

            Assert.True(valido, "Instituição deveria ser válida");
        }

        [Theory]
        [InlineData("", "Descrição de teste")] // nome inválido
        [Trait("Categoria", "Instituicoes")]
        public void Cadastrar_InstituicaoValida_ReturnFalse(string nome, string descricao)
        {
            _nome = nome;
            _descricao = descricao;

            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();

            Assert.False(valido, "Instituição deveria ser inválida");
        }

        private Instituicao CriarInstituicao()
        {
            var instituicao = new Instituicao(_nome, _sigla, _descricao, _cnpj, _organizacaoAcademica, _rede, _categoriaAdministrativa, _nivelEnsino, _cep, _municipio, _uf);

            return instituicao;
        }

       
    }
}
