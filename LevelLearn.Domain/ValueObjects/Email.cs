using FluentValidation;
using LevelLearn.Domain.Validators;

namespace LevelLearn.Domain.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        protected Email() { }

        public Email(string address)
        {
            this.Address = address?.Trim()?.ToLower();
        }

        public string Address { get; private set; }

        public override bool EstaValido()
        {
            RuleFor(e => e.Address)
                .NotNull().WithMessage("E-mail precisa ser preenchido")
                .MaximumLength(PropertiesConfig.EMAIL_TAMANHO_MAXIMO)
                .WithMessage($"E-mail precisa ser ter no máximo {PropertiesConfig.EMAIL_TAMANHO_MAXIMO} caracteres")
                .EmailAddress().WithMessage("E-mail não é válido");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Address;
        }

    }
}
