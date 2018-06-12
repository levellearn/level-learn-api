using System.ComponentModel;

namespace LevelLearn.ViewModel.Institucional.Turma
{
    public class ViewTurmaViewModel
    {
        [DisplayName("Turma")]
        public int TurmaId { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
