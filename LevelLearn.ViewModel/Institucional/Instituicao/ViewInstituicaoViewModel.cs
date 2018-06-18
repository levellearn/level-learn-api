using System.ComponentModel;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class ViewInstituicaoViewModel
    {
        [DisplayName("Instituição")]
        public int InstituicaoId { get; set; }

        [DisplayName("Instituição")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
