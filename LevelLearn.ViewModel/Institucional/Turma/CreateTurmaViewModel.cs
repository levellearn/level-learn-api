using System.Collections.Generic;
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

        [DisplayName("Curso")]
        [Required(ErrorMessage = "O campo Curso é obrigatório")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O campo Curso é obrigatório")]
        public int CursoId { get; set; }

        [DisplayName("Alunos")]
        public List<int> AlunoIds { get; set; } = new List<int>();
    }
}
