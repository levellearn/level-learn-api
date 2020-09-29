
using LevelLearn.Domain.Validators.ValueObjects;

namespace LevelLearn.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco?.Trim()?.ToLower() ?? string.Empty;
        }

        public string Endereco { get; private set; }

        public override bool EstaValido()
        {
            var validator = new EmailValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        #region Overrides

        public override string ToString()
        {
            return this.Endereco;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Email;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Endereco.Equals(compareTo.Endereco);
        }

        public static bool operator ==(Email a, Email b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Email a, Email b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Endereco.GetHashCode();
        }

        #endregion

    }
}
