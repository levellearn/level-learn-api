using FluentValidation;
using LevelLearn.Domain.Extensions;
using System;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.ValueObjects
{
    public class Celular : ValueObject
    {
        protected Celular() { }

        public Celular(string numero)
        {
            Numero = numero.GetNumbers();
            Numero = Numero.StartsWith("55") ? Numero : Numero.Insert(0, "55");
        }

        public string Numero { get; private set; }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Numero)) return false;

            string pattern = @"^(55)([1-9][0-9])(\d{5})(\d{4})$";

            if (Regex.IsMatch(Numero, pattern))
                return true;
            else
                return false;
        }

        public override bool EstaValido()
        {
            RuleFor(c => c.Numero)
                .Must(a => Validate())
                    .WithMessage("")
                    .When(p => !string.IsNullOrWhiteSpace(p.Numero))
                .OverridePropertyName("Celular");

            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(this.Numero).ToString("+##(##)#####-####");
        }
    }
}
