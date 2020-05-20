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

        #region Usuario
        public string UsuarioLoginSucesso { get; }
        public string UsuarioLoginFalha { get; }
        public string UsuarioLogoutSucesso { get; }
        public string UsuarioEmailJaExiste { get; }
        public string UsuarioContaBloqueada { get; }
        public string UsuarioEmailNaoConfirmado { get; }
        public string UsuarioEmailConfirmarFalha { get; }
        public string UsuarioEmailConfirmarSucesso { get; }
        public string UsuarioSenhaObrigatoria { get; }
        public string UsuarioSenhaTamanho(params object[] arguments);
        public string UsuarioConfirmacaoSenhaObrigatoria { get; }
        public string UsuarioConfirmacaoSenhaNaoConfere { get; }
        public string UsuarioSenhaRequerMaiusculo { get; }
        public string UsuarioSenhaRequerMinusculo { get; }
        public string UsuarioSenhaRequerDigito { get; }
        public string UsuarioSenhaRequerEspecial { get; }
        public string UsuarioEmailObrigatorio { get; }
        public string UsuarioEmailTamanhoMaximo(int argument);
        public string UsuarioEmailInvalido { get; }

        #endregion

        #region Pessoa
        public string PessoaCPFJaExiste { get; }
        public string PessoaDataNascimentoInvalida { get; }
        public string PessoaGeneroObrigatorio { get; }
        public string PessoaImagemObrigatoria { get; }
        public string PessoaNickNameInvalido { get; }
        public string PessoaNickNameObrigatorio { get; }
        public string PessoaNickNameTamanhoMaximo(int argument);
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
