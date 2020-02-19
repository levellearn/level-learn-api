using FluentValidation;
using LevelLearn.Domain.Extensions;
using System;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.ValueObjects
{
    public class Celular : ValueObject<Celular>
    {
        protected Celular() { }

        public Celular(string number)
        {
            CellPhoneNumber = number.GetNumbers();
            CellPhoneNumber = CellPhoneNumber.StartsWith("55") ? CellPhoneNumber : CellPhoneNumber.Insert(0, "55");
        }

        public string CellPhoneNumber { get; private set; }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(CellPhoneNumber)) return false;

            string pattern = @"^(55)([1-9][0-9])(\d{5})(\d{4})$";

            if (Regex.IsMatch(CellPhoneNumber, pattern))
                return true;
            else
                return false;
        }

        public override bool EstaValido()
        {
            RuleFor(c => c.CellPhoneNumber)
                .Must(a => Validate()).WithMessage("Celular não é válido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(this.CellPhoneNumber).ToString("+##(##)#####-####");
        }
    }
}
