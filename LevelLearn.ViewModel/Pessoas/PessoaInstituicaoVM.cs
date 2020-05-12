using LevelLearn.ViewModel.Enums;
using System;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaInstituicaoVM
    {
        //public Guid Id { get; set; }
        public PerfisInstituicaoVM Perfil { get; set; }

        //public Guid PessoaId { get; set; }
        public virtual PessoaVM Pessoa { get; set; }

        //public Guid InstituicaoId { get; set; }
        //public virtual Instituicao Instituicao { get; set; }
    }
}
