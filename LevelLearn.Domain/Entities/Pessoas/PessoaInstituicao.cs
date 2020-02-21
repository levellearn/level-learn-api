using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class PessoaInstituicao
    {
        protected PessoaInstituicao() { }

        //public int PessoaInstituicaoId { get; set; }
        public PerfisInstituicao Perfil { get; set; }

        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public Guid InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
    }
}
