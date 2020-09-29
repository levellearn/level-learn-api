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

        #region Overrides

        public override string ToString()
        {
            if (!int.TryParse(Numero, out int numeroConvertido) || !ResultadoValidacao.IsValid)
                return this.Numero;

            return numeroConvertido.ToString("+##(##)#####-####");
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Celular;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Numero.Equals(compareTo.Numero);
        }

        public static bool operator ==(Celular a, Celular b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Celular a, Celular b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Numero.GetHashCode();
        }

        #endregion

    }
}
