using System;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CursoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public Guid InstituicaoId { get; set; }

        //public virtual InstituicaoVM Instituicao { get; set; }
        //public virtual ICollection<PessoaCurso> Pessoas { get; private set; }
    }
}
