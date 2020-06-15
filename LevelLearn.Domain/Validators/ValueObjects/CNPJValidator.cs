using FluentValidation;
using LevelLearn.Domain.ValueObjects;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class CNPJValidator : AbstractValidator<CNPJ>
    {
        // TODO: Add resource?
        public CNPJValidator()
        {
            RuleFor(c => c.Numero)
                .Must(c => ValidarNumero(c))
                    .WithMessage("CNPJ não é válido")
                    .When(c => !string.IsNullOrWhiteSpace(c.Numero))
                .OverridePropertyName("CNPJ");
        }

        private bool ValidarNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero)) return false;

            if (IsInBlackList(numero)) return false;

            string cnpj = numero;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private bool IsInBlackList(string number)
        {
            if (number.Equals("00000000000000") ||
                    number.Equals("11111111111111") ||
                    number.Equals("22222222222222") ||
                    number.Equals("33333333333333") ||
                    number.Equals("44444444444444") ||
                    number.Equals("55555555555555") ||
                    number.Equals("66666666666666") ||
                    number.Equals("77777777777777") ||
                    number.Equals("88888888888888") ||
                    number.Equals("99999999999999"))
                return true;

            return false;
        }


    }
}
