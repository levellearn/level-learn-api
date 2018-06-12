using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class UpdateInstituicaoViewModel
    {
        [Required(ErrorMessage = "Erro ao obter o código de verificação")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Erro ao obter o código de verificação")]
        public int InstituicaoId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
