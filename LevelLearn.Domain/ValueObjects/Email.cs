
using LevelLearn.Domain.Validators.ValueObjects;

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
            var validator = new EmailValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            return Endereco;
        }

    }
}
