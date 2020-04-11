using FluentValidation;
using FluentValidation.Results;

namespace LevelLearn.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        protected ValueObject()
        {
            ResultadoValidacao = new ValidationResult();
        }

        public ValidationResult ResultadoValidacao { get; set; }

        public abstract bool EstaValido();
    }
}
