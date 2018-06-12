using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Turma
{
    public class UpdateTurmaViewModel
    {
        [Required(ErrorMessage = "Erro ao obter o código de verificação")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Erro ao obter o código de verificação")]
        public int TurmaId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
