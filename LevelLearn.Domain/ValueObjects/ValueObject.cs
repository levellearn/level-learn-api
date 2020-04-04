using FluentValidation;
using FluentValidation.Results;

namespace LevelLearn.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        protected ValueObject()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; set; }

        public abstract bool EstaValido();
    }
}
