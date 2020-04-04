using FluentValidation;
using LevelLearn.Domain.Validators;

namespace LevelLearn.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco?.Trim()?.ToLower() ?? string.Empty;
        }

        public string Endereco { get; private set; }

        public override bool EstaValido()
        {
            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Endereco;
        }

    }
}
