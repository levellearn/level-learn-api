namespace LevelLearn.Domain.Validators
{
    public class DadoInvalido
    {
        public DadoInvalido(string nomePropriedade, string mensagemErro)
        {
            NomePropriedade = nomePropriedade;
            MensagemErro = mensagemErro;
        }

        public string NomePropriedade { get; private set; }
        public string MensagemErro { get; private set; }

    }
}
