using LevelLearn.ViewModel.Enum;

namespace LevelLearn.ViewModel.Pessoas.PessoaCurso
{
    public class ViewPessoaCursoViewModel
    {
        public int PessoaCursoId { get; set; }
        public TipoPessoaEnumViewModel Perfil { get; set; }

        public int PessoaId { get; set; }

        public int CursoId { get; set; }
    }
}
