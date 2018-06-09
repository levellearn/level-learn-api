using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;

namespace LevelLearn.Domain.Pessoas
{
    public class PessoaInstituicao
    {
        public int PessoaInstituicaoId { get; set; }
        public PerfilInstituicaoEnum Perfil { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}
