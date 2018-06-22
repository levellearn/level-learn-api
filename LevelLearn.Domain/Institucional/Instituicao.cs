using LevelLearn.Domain.Pessoas;
using System.Collections.Generic;

namespace LevelLearn.Domain.Institucional
{
    public class Instituicao
    {
        public int InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public List<Curso> Cursos { get; set; } = new List<Curso>();
        public List<PessoaInstituicao> Pessoas { get; set; } = new List<PessoaInstituicao>();
    }
}