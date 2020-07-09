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

        #region Overrides

        public override string ToString()
        {
            if (!int.TryParse(Numero, out int numeroConvertido) || !ResultadoValidacao.IsValid)
                return this.Numero;

            return numeroConvertido.ToString("##\\.###\\.###/####-##").PadLeft(18, '0');
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as CNPJ;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Numero.Equals(compareTo.Numero);
        }

        public static bool operator ==(CNPJ a, CNPJ b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(CNPJ a, CNPJ b)
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
