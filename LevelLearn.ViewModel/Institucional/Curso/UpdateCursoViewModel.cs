using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class UpdateCursoViewModel
    {
        [Required(ErrorMessage = "Erro ao obter o código de verificação")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Erro ao obter o código de verificação")]
        public int CursoId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Instituição")]
        [Required(ErrorMessage = "O campo Instituição é obrigatório")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O campo Instituição é obrigatório")]
        public int InstituicaoId { get; set; }
    }
}
