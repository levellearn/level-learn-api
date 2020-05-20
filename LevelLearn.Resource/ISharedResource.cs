namespace LevelLearn.Resource
{
    public interface ISharedResource
    {

        /// <summary>
        /// Obtém um recurso
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>

        public string GetResource(string resourceKey);

        /// <summary>
        /// Obtém um recurso e faz as substituições nas mensagens
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>
        /// <param name="replacements">Valores a ser usados para substituir na mensagem do recurso</param>
        public string GetResource(string resourceName, params object[] replacements);


        #region Geral
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
        #endregion       

        #region Pessoa
        public string PessoaCPFJaExiste { get; }
        public string PessoaDataNascimentoInvalida { get; }
        public string PessoaGeneroObrigatorio { get; }      
        public string PessoaNomeObrigatorio { get; }
        public string PessoaNomePrecisaSobrenome { get; }
        public string PessoaNomeTamanho(params object[] arguments);
        public string PessoaTipoPessoaInvalido { get; }
        public string PessoaCelularInvalido { get; }
        public string PessoaCPFInvalido { get; }
        public string AlunoRAObrigatorio { get; }
        public string ProfessorCPFObrigatorio { get; }

        #endregion       

    }
}
