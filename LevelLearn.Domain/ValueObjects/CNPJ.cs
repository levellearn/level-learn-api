using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.ValueObjects;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class CNPJ : ValueObject
    {
        protected CNPJ() { }

        public CNPJ(string numero)
        {
            this.Numero = numero.GetNumbers();
        }

        public string Numero { get; private set; }

        public override bool EstaValido()
        {
            var validator = new CNPJValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            int.TryParse(Numero, out int numeroConvertido);

            return numeroConvertido.ToString("##\\.###\\.###/####-##").PadLeft(18, '0');
        }

    }
}
