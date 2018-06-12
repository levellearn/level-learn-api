using LevelLearn.ViewModel.Institucional.Instituicao;
using System.ComponentModel;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class ViewCursoViewModel
    {
        [DisplayName("Curso")]
        public int CursoId { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Instituição")]
        public int InstituicaoId { get; set; }
        public ViewInstituicaoViewModel Instituicao { get; set; }
    }
}
