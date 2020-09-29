using FluentValidation.Results;
using LevelLearn.Domain.Validators;
using Microsoft.AspNetCore.Identity;
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

        public static ICollection<DadoInvalido> GetErrorsResult(this IdentityResult identityResult)
        {
            var errorsResult = new List<DadoInvalido>();

            foreach (var error in identityResult.Errors)
            {
                errorsResult.Add(new DadoInvalido(error.Code, error.Description));
            }

            return errorsResult;
        }

    }
}
