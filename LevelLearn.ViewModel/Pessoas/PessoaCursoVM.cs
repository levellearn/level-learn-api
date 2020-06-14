using LevelLearn.Domain.Enums;

namespace LevelLearn.ViewModel.Pessoas
{
    public class PessoaCursoVM
    {
        public TipoPessoa Perfil { get; set; }
        public virtual PessoaVM Pessoa { get; set; }
    }
}
