using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.ValueObjects;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class Celular : ValueObject
    {
        protected Celular() { }

        public Celular(string numero)
        {
            Numero = numero.GetNumbers();
        }


        public string Numero { get; private set; }


        public override bool EstaValido()
        {
            var validator = new CelularValidator();
            ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            if (!int.TryParse(Numero, out int numeroConvertido) || !ResultadoValidacao.IsValid)
                return this.Numero;

            return numeroConvertido.ToString("+##(##)#####-####");
        }

    }
}
