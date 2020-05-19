namespace LevelLearn.Domain.Validators
{
    /// <summary>
    /// Representa um dado inválido de uma entidade
    /// </summary>
    public class DadoInvalido
    {
        /// <summary>
        /// Cria um objeto para os dados inválidos de uma entidade
        /// </summary>
        /// <param name="propriedade">Nome da propriedade</param>
        /// <param name="mensagemErro">Mensagem de erro da propriedade</param>
        public DadoInvalido(string propriedade, string mensagemErro)
        {
            Propriedade = propriedade;
            MensagemErro = mensagemErro;
        }

        public string Propriedade { get; private set; }
        public string MensagemErro { get; private set; }

    }
}
