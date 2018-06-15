using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Pessoas.Pessoa
{
    public class LoginPessoaViewModel
    {
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
