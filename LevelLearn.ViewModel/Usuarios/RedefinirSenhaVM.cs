using LevelLearn.ViewModel.Comum;
using LevelLearn.ViewModel.Usuarios.Validators;

namespace LevelLearn.ViewModel.Usuarios
{
    public class RedefinirSenhaVM : BaseVM
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmacaoSenha { get; set; }

        public override bool EstaValido()
        {
            var validator = new RedefinirSenhaVMValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return this.ResultadoValidacao.IsValid;
        }

    }

}
