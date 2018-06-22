using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CreateCursoViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Instituição")]
        [Required(ErrorMessage = "O campo Instituição é obrigatório")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O campo Instituição é obrigatório")]
        public int InstituicaoId { get; set; }

        [DisplayName("Professores")]
        public List<int> Professores { get; set; } = new List<int>();

        [DisplayName("Alunos")]
        public List<int> Alunos { get; set; } = new List<int>();
    }
}
