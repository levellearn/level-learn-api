using FluentValidation;
using LevelLearn.Domain.Entities.Usuarios;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class UsuarioValidator : AbstractValidator<ApplicationUser>
    {
        public UsuarioValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;

            ValidarEmail();
            ValidarUserName();
            ValidarCelular();
            ValidarSenha();
        }

        private void ValidarUserName()
        {
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX + "}$";

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("Username precisa estar preenchido")
                .Must(p => Regex.IsMatch(p, pattern))
                .WithMessage("Username somente deve conter letras, números, (_), (-) ou (.)")
                .MaximumLength(PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX)
                .WithMessage($"Username pode ter no máximo {PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX} caracteres");
        }

        private void ValidarEmail()
        {
            RuleFor(p => p.Email)
                .NotNull().WithMessage("E-mail precisa estar preenchido")
                .MaximumLength(PropertiesConfig.Pessoa.EMAIL_TAMANHO_MAX)
                    .WithMessage($"E-mail pode ter no máximo {PropertiesConfig.Pessoa.EMAIL_TAMANHO_MAX} caracteres")
                .EmailAddress().WithMessage("E-mail não é válido");
        }

        private void ValidarCelular()
        {
            RuleFor(c => c.PhoneNumber)
                .NotNull()
                    .WithMessage("Celular precisa estar preenchido")
                .Matches(@"^(55)([1-9][0-9])(\d{5})(\d{4})$")
                    .WithMessage("Celular não é válido");
        }

        private void ValidarSenha()
        {
            RuleFor(p => p.Senha)
                .NotNull()
                    .WithMessage("Senha precisa estar preenchida")
                .Equal(p => p.ConfirmacaoSenha)
                    .WithMessage("As senhas não conferem");
        }

    }
}
