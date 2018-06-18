using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class CreateInstituicaoViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Administradores")]
        public List<int> Admins { get; set; } = new List<int>();

        [DisplayName("Professores")]
        public List<int> Professores { get; set; } = new List<int>();

        [DisplayName("Alunos")]
        public List<int> Alunos { get; set; } = new List<int>();
    }
}
