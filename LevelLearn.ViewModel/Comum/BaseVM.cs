using FluentValidation.Results;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using System.Collections.Generic;

namespace LevelLearn.ViewModel.Comum
{
    public abstract class BaseVM
    {
        public ValidationResult ResultadoValidacao { get; set; }

        public abstract bool EstaValido();
        
        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ResultadoValidacao.GetErrorsResult();
        }

    }
}
