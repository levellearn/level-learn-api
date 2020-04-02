using FluentValidation;
using LevelLearn.Domain.Validators;

namespace LevelLearn.Domain.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco?.Trim()?.ToLower() ?? string.Empty;
        }

        public string Endereco { get; private set; }

        public override bool EstaValido()
        {
            RuleFor(e => e.Endereco)
                .NotEmpty().WithMessage("E-mail precisa estar preenchido")
                .MaximumLength(RegraAtributo.Pessoa.EMAIL_TAMANHO_MAX)
                    .WithMessage($"E-mail pode ter no máximo {RegraAtributo.Pessoa.EMAIL_TAMANHO_MAX} caracteres")
                .EmailAddress().WithMessage("E-mail não é válido")
                .OverridePropertyName("Email");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Endereco;
        }

    }
}
