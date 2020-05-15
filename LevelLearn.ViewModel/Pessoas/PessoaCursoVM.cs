using LevelLearn.ViewModel.Enums;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaCursoVM
    {
        public TiposPessoaVM Perfil { get; set; }
        public virtual PessoaVM Pessoa { get; set; }
    }
}
