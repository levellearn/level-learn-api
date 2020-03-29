using FluentValidation;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class CPF : ValueObject<CPF>
    {
        protected CPF() { }

        public CPF(string numero)
        {
            this.Numero = numero.GetNumbers();
        }

        public string Numero { get; private set; }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Numero)) return false;

            if (IsInBlackList(Numero)) return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            Numero = Numero.Trim();
            Numero = Numero.Replace(".", "").Replace("-", "");
            if (Numero.Length != 11)
                return false;
            var tempCpf = Numero.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return Numero.EndsWith(digito);
        }

        public bool IsInBlackList(string number)
        {
            if (number.Equals("00000000000") ||
                    number.Equals("11111111111") ||
                    number.Equals("22222222222") ||
                    number.Equals("33333333333") ||
                    number.Equals("44444444444") ||
                    number.Equals("55555555555") ||
                    number.Equals("66666666666") ||
                    number.Equals("77777777777") ||
                    number.Equals("88888888888") ||
                    number.Equals("99999999999"))
                return true;

            return false;
        }

        public override bool EstaValido()
        {
            RuleFor(c => c.Numero)
                .Must(a => Validate())
                    .WithMessage("CPF não é válido")
                    .When(p => !string.IsNullOrWhiteSpace(p.Numero))
                .OverridePropertyName("CPF");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(Numero).ToString("###\\.###\\.###-##").PadLeft(14, '0');
        }

    }
}
