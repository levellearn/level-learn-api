using LevelLearn.Domain.Enums;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class CadastrarInstituicaoVM
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
        public OrganizacaoAcademica OrganizacaoAcademica { get; set; }
        public Rede Rede { get; set; }
        public CategoriaAdministrativa CategoriaAdministrativa { get; set; }
        public NivelEnsino NivelEnsino { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
    }
}
