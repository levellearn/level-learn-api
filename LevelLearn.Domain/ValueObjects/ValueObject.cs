using FluentValidation;
using FluentValidation.Results;

namespace LevelLearn.Domain.ValueObjects
{
    public abstract class ValueObject<T> : AbstractValidator<T> where T : ValueObject<T>
    {
        protected ValueObject()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool EstaValido();
    }
}
