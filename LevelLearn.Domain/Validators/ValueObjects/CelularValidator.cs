using FluentValidation;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class CelularValidator : AbstractValidator<Celular>
    {
        private readonly PessoaResource _resource;

        public CelularValidator()
        {
            _resource = PessoaResource.ObterInstancia();

            RuleFor(c => c.Numero)
                .Must(c => ValidarNumero(c))
                    .WithMessage(_resource.PessoaCelularInvalido)
                    .When(c => !string.IsNullOrWhiteSpace(c.Numero))
                .OverridePropertyName("Celular");
        }

        public bool ValidarNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero)) return false;

            string pattern = @"^(\d{2})([1-9][0-9])(\d{5})(\d{4})$";

            if (Regex.IsMatch(numero, pattern))
                return true;

            return false;
        }



    }
}
