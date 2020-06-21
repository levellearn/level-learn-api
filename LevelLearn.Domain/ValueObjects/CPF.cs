using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.ValueObjects;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class CPF : ValueObject
    {
        protected CPF() { }

        public CPF(string numero)
        {
            this.Numero = numero.GetNumbers();
        }


        public string Numero { get; private set; }


        public override bool EstaValido()
        {
            var validator = new CPFValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            if (!int.TryParse(Numero, out int numeroConvertido) || !ResultadoValidacao.IsValid)
                return this.Numero;

            return numeroConvertido.ToString("###\\.###\\.###-##").PadLeft(14, '0');
        }

    }
}
