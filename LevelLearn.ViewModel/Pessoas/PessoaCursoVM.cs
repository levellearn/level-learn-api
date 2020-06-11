using LevelLearn.ViewModel.Enums;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaCursoVM
    {
        public TipoPessoaVM Perfil { get; set; }
        public virtual PessoaVM Pessoa { get; set; }
    }
}
