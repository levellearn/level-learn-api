using LevelLearn.Domain.Validators.ValueObjects;

namespace LevelLearn.Domain.ValueObjects
{
    public class Senha : ValueObject
    {
        protected Senha() { }

        public Senha(string segredo)
        {
            this.Segredo = segredo ?? string.Empty;
        }

        public string Segredo { get; private set; }

        public override bool EstaValido()
        {
            var validator = new SenhaValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }
       

        #region Overrides

        public override string ToString()
        {
            return Segredo;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Senha;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Segredo.Equals(compareTo.Segredo);
        }

        public static bool operator ==(Senha a, Senha b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Senha a, Senha b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Segredo.GetHashCode();
        }

        #endregion


    }
}
