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
            ValidarNickName();
            ValidarCelular();
            ValidarSenha();
        }

        private void ValidarNickName()
        {
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + PropertiesConfig.Pessoa.NICKNAME_TAMANHO_MAX + "}$";

            RuleFor(p => p.NickName)
                .NotEmpty().WithMessage("NickName precisa estar preenchido")
                .Must(p => Regex.IsMatch(p, pattern))
                .WithMessage("NickName somente deve conter letras, números, (_), (-) ou (.)")
                .MaximumLength(PropertiesConfig.Pessoa.NICKNAME_TAMANHO_MAX)
                .WithMessage($"NickName pode ter no máximo {PropertiesConfig.Pessoa.NICKNAME_TAMANHO_MAX} caracteres");
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
                .Length(PropertiesConfig.Pessoa.SENHA_TAMANHO_MIN, PropertiesConfig.Pessoa.SENHA_TAMANHO_MAX)
                    .WithMessage($"Senha precisa estar entre {PropertiesConfig.Pessoa.SENHA_TAMANHO_MIN} e {PropertiesConfig.Pessoa.SENHA_TAMANHO_MAX} caracteres")
                .Equal(p => p.ConfirmacaoSenha)
                    .WithMessage("As senhas não conferem");
        }

    }
}
