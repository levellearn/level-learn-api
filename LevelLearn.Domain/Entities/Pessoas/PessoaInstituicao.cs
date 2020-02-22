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
        }

        public Guid Id { get; private set; }
        public PerfisInstituicao Perfil { get; private set; }

        public Guid PessoaId { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }

        public Guid InstituicaoId { get; private set; }
        public virtual Instituicao Instituicao { get; private set; }

    }
}
