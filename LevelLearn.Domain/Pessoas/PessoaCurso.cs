using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;

namespace LevelLearn.Domain.Pessoas
{
    public class PessoaCurso
    {
        public int PessoaCursoId { get; set; }
        public TipoPessoaEnum Perfil { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
