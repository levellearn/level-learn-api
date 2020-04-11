using LevelLearn.Domain.Extensions;
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
            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            return Convert.ToInt64(Numero).ToString("###\\.###\\.###-##").PadLeft(14, '0');
        }

    }
}
