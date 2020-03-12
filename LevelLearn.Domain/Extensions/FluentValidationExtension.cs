using FluentValidation.Results;
using LevelLearn.Domain.Entities;
using LevelLearn.Domain.Validators;
using System.Collections.Generic;

namespace LevelLearn.Domain.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddErrors(this ValidationResult validationResult, ValidationResult otherValidationResult)
        {
            foreach (var error in otherValidationResult.Errors)
            {
                validationResult.Errors.Add(error);
            }
        }

        public static ICollection<DadoInvalido> GetErrorsResult(this ValidationResult validationResult)
        {
            var errorsResult = new List<DadoInvalido>();

            foreach (var error in validationResult.Errors)
            {
                errorsResult.Add(new DadoInvalido(error.PropertyName, error.ErrorMessage));
            }

            return errorsResult;
        }

    }
}
