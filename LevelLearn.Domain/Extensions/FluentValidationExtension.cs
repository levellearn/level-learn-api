using FluentValidation.Results;
using LevelLearn.Domain.Entities;
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

        //public static ICollection<DadoInvalidoEntidade> GetErrorsResult(this ValidationResult validationResult)
        //{
        //    var errorsResult = new List<DadoInvalidoEntidade>();

        //    foreach (var error in validationResult.Errors)
        //    {
        //        errorsResult.Add(new DadoInvalidoEntidade(error.PropertyName, error.ErrorMessage));
        //    }

        //    return errorsResult;
        //}

    }
}
