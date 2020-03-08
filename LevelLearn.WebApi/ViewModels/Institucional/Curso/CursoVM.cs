using System;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class CursoVM
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public Guid InstituicaoId { get; set; }
        public virtual InstituicaoVM Instituicao { get; set; }
        //public virtual ICollection<PessoaCurso> Pessoas { get; private set; }
    }
}
