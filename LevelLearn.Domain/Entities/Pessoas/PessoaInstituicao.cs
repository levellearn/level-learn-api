using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class PessoaInstituicao
    {
        protected PessoaInstituicao() { }

        public PessoaInstituicao(PerfisInstituicao perfil, Guid pessoaId, Guid instituicaoId)
        {
            Perfil = perfil;
            PessoaId = pessoaId;
            InstituicaoId = instituicaoId;
            DataCadastro = DateTime.UtcNow;
        }

        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public Guid InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }

        public PerfisInstituicao Perfil { get; set; }
        public DateTime DataCadastro { get; private set; }

    }
}
