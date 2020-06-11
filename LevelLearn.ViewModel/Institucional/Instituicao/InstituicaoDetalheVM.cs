using LevelLearn.Domain.Enums;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Pessoas;
using System;
using System.Collections.Generic;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class InstituicaoDetalheVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public string Cnpj { get; set; }
        public OrganizacaoAcademica OrganizacaoAcademica { get; set; }
        public Rede Rede { get; set; }
        public CategoriaAdministrativa CategoriaAdministrativa { get; set; }
        public NivelEnsino NivelEnsino { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        public ICollection<CursoVM> Cursos { get; set; }
        public ICollection<PessoaInstituicaoVM> Pessoas { get; set; }
    }
}
