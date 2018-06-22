using LevelLearn.Domain.Pessoas;
using System.Collections.Generic;

namespace LevelLearn.Domain.Institucional
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }

        public List<PessoaCurso> Pessoas { get; set; } = new List<PessoaCurso>();
    }
}
