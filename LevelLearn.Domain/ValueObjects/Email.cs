using FluentValidation;
using LevelLearn.Domain.Validators;

namespace LevelLearn.Domain.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco?.Trim()?.ToLower();
        }

        public string Endereco { get; private set; }

        public override bool EstaValido()
        {
            RuleFor(e => e.Endereco)
                .NotNull().WithMessage("E-mail precisa estar preenchido")
                .MaximumLength(PropertiesConfig.Pessoa.EMAIL_TAMANHO_MAX)
                    .WithMessage($"E-mail pode ter no máximo {PropertiesConfig.Pessoa.EMAIL_TAMANHO_MAX} caracteres")
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
