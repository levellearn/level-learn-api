using LevelLearn.Domain.Extensions;
using System;

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

        public override bool EstaValido()
        {
            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(this.Numero).ToString("+##(##)#####-####");
        }
    }
}
