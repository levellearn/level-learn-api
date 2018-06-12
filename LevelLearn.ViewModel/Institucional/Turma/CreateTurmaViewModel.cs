using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Turma
{
    public class CreateTurmaViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
