using FluentValidation.Results;

namespace LevelLearn.Domain.Validators
{
    public interface IValidador<T> where T : class
    {
        ValidationResult Validar(T instance);
    }
}
