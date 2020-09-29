namespace LevelLearn.Resource
{
    public interface ISharedResource
    {

        /// <summary>
        /// Obtém um recurso
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>
        public string ObterResource(string resourceKey);

        /// <summary>
        /// Obtém um recurso e faz as substituições nas mensagens
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>
        /// <param name="replacements">Valores a ser usados para substituir na mensagem do recurso</param>
        public string ObterResource(string resourceName, params object[] replacements);

        public string ErroInternoServidor { get; }
        public string CadastradoSucesso { get; }
        public string AtualizadoSucesso { get; }
        public string DeletadoSucesso { get; }
        public string DadosInvalidos { get; }
        public string NaoEncontrado { get; }
        public string FalhaCadastrar { get; }
        public string FalhaAtualizar { get; }
        public string FalhaDeletar { get; }
        public string IdObrigatorio { get; }        

    }
}
