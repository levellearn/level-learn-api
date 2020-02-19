using FluentValidation;
using LevelLearn.Domain.Extensions;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class RA : ValueObject<RA>
    {
        protected RA() { }

        public RA(string numero)
        {
            this.Numero = numero.GetNumbers();
        }

        public string Numero { get; private set; }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Numero)) return false;

            return true;
        }        

        public override bool Valido()
        {
            RuleFor(p => p.Numero)
                .Must(p => Validate()).WithMessage("RA não é válido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        //public override string ToString()
        //{
        //    return Convert.ToInt64(Numero).ToString("###\\.###\\.###-##").PadLeft(14, '0');
        //}

    }
}
