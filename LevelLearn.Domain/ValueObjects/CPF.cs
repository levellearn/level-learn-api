﻿using FluentValidation;
using LevelLearn.Domain.Extensions;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class CPF : ValueObject<CPF>
    {
        protected CPF() { }

        public CPF(string number)
        {
            this.Number = number.GetNumbers();
        }

        public string Number { get; private set; }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Number)) return false;

            if (IsInBlackList(Number)) return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            Number = Number.Trim();
            Number = Number.Replace(".", "").Replace("-", "");
            if (Number.Length != 11)
                return false;
            var tempCpf = Number.Substring(0, 9);
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
            return Number.EndsWith(digito);
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
            RuleFor(c => c.Number)
                .Must(a => Validate()).WithMessage("CPF não é válido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(Number).ToString("###\\.###\\.###-##").PadLeft(14, '0');
        }

    }
}