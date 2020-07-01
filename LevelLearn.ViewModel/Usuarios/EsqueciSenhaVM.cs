using LevelLearn.Domain.Validators.RegrasAtributos;
using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Usuarios
{
    public class EsqueciSenhaVM
    {
        //[Required]
        //[StringLength(RegraUsuario.EMAIL_TAMANHO_MAX)]
        //[EmailAddress]
        public string Email { get; set; }
    }
}
