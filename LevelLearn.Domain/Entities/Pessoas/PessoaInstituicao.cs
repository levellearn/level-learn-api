using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class PessoaInstituicao
    {
        protected PessoaInstituicao() { }

        public PessoaInstituicao(PerfilInstituicao perfil, Guid pessoaId, Guid instituicaoId)
        {
            Perfil = perfil;
            PessoaId = pessoaId;
            InstituicaoId = instituicaoId;
            DataCadastro = DateTime.UtcNow;
        }

        public Guid PessoaId { get; private set; }
        public Pessoa Pessoa { get; set; }

        public Guid InstituicaoId { get; private set; }
        public Instituicao Instituicao { get; set; }

        public PerfilInstituicao Perfil { get; private set; }
        public DateTime DataCadastro { get; private set; }

    }
}
