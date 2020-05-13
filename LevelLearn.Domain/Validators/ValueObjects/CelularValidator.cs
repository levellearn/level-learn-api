using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class CelularValidator : AbstractValidator<Celular>, IValidador<Celular>
    {
        private readonly ISharedResource _sharedResource;

        public CelularValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        public ValidationResult Validar(Celular instance)
        {
            RuleFor(c => c.Numero)
                .Must(c => ValidarNumero(c))
                    .WithMessage(_sharedResource.PessoaCelularInvalido)
                    .When(c => !string.IsNullOrWhiteSpace(c.Numero))
                .OverridePropertyName("Celular");

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }

        private bool ValidarNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero)) return false;

            string pattern = @"^(55)([1-9][0-9])(\d{5})(\d{4})$";

            if (Regex.IsMatch(numero, pattern))
                return true;
            else
                return false;
        }



    }
}
