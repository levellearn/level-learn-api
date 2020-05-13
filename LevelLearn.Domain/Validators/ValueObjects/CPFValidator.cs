using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class CPFValidator : AbstractValidator<CPF>, IValidador<CPF>
    {
        private readonly ISharedResource _sharedResource;

        public CPFValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        public ValidationResult Validar(CPF instance)
        {
            RuleFor(c => c.Numero)
                .Must(c => ValidarNumero(c))
                    .WithMessage(_sharedResource.PessoaCPFInvalido)
                    .When(c => !string.IsNullOrWhiteSpace(c.Numero))
                .OverridePropertyName("CPF");

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }


        private bool ValidarNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero)) return false;

            if (IsInBlackList(numero)) return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            numero = numero.Trim();
            numero = numero.Replace(".", "").Replace("-", "");

            if (numero.Length != 11)
                return false;

            var tempCpf = numero.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return numero.EndsWith(digito);
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


    }
}
