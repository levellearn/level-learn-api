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
            Id = Guid.NewGuid();
            Perfil = perfil;
            PessoaId = pessoaId;
            InstituicaoId = instituicaoId;
            DataCadastro = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public PerfisInstituicao Perfil { get; set; }

        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public Guid InstituicaoId { get; set; }
        public DateTime? DataCadastro { get; private set; }
        public virtual Instituicao Instituicao { get; set; }

    }
}
