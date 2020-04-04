using FluentValidation.Results;

namespace LevelLearn.Domain.Validators
{
    public interface IValidatorApp<T> where T : class
    {
        ValidationResult Validar(T instance);
    }
}
