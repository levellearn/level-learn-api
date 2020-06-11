using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class InstituicaoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public string Cnpj { get; set; }
        public string OrganizacaoAcademica { get; set; }
        public string Rede { get; set; }
        public string CategoriaAdministrativa { get; set; }
        public string NivelEnsino { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
