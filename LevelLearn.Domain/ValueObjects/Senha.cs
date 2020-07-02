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

        public override string ToString()
        {
            return Segredo;
        }

    }
}
