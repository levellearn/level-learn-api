namespace LevelLearn.Resource.Usuarios
{
    public class UsuarioResource : ResourceBase
    {
        private static UsuarioResource _instancia;

        private UsuarioResource() : base(typeof(UsuarioResource))
        {
        }

        /// <summary>
        /// Obtém uma instância única de UsuarioResource (Singleton)
        /// </summary>
        /// <returns></returns>
        public static UsuarioResource ObterInstancia()
        {
            if (_instancia == null)
                _instancia = new UsuarioResource();

            return _instancia;
        }

        public string UsuarioLoginSucesso => ObterResource(nameof(UsuarioLoginSucesso));
        public string UsuarioLoginFalha => ObterResource(nameof(UsuarioLoginFalha));
        public string UsuarioLogoutSucesso => ObterResource(nameof(UsuarioLogoutSucesso));
        public string UsuarioEmailJaExiste => ObterResource(nameof(UsuarioEmailJaExiste));
        public string UsuarioContaBloqueada => ObterResource(nameof(UsuarioContaBloqueada));
        public string UsuarioEmailNaoConfirmado => ObterResource(nameof(UsuarioEmailNaoConfirmado));
        public string UsuarioEmailConfirmarFalha => ObterResource(nameof(UsuarioEmailConfirmarFalha));
        public string UsuarioEmailConfirmarSucesso => ObterResource(nameof(UsuarioEmailConfirmarSucesso));
        public string UsuarioRedefinirSenhaFalha => ObterResource(nameof(UsuarioRedefinirSenhaFalha));
        public string UsuarioRedefinirSenhaSucesso => ObterResource(nameof(UsuarioRedefinirSenhaSucesso));
        public string UsuarioAlterarSenhaFalha => ObterResource(nameof(UsuarioAlterarSenhaFalha));
        public string UsuarioAlterarSenhaSucesso => ObterResource(nameof(UsuarioAlterarSenhaSucesso));

        public string UsuarioSenhaObrigatoria => ObterResource(nameof(UsuarioSenhaObrigatoria));
        public string UsuarioSenhaTamanho(params object[] arguments) => ObterResource(nameof(UsuarioSenhaTamanho), arguments);
        public string UsuarioConfirmacaoSenhaNaoConfere => ObterResource(nameof(UsuarioConfirmacaoSenhaNaoConfere));
        public string UsuarioSenhaRequerMaiusculo => ObterResource(nameof(UsuarioSenhaRequerMaiusculo));
        public string UsuarioSenhaRequerMinusculo => ObterResource(nameof(UsuarioSenhaRequerMinusculo));
        public string UsuarioSenhaRequerDigito => ObterResource(nameof(UsuarioSenhaRequerDigito));
        public string UsuarioSenhaRequerEspecial => ObterResource(nameof(UsuarioSenhaRequerEspecial));

        public string UsuarioEmailObrigatorio => ObterResource(nameof(UsuarioEmailObrigatorio));
        public string UsuarioEmailTamanhoMaximo(int argument) => ObterResource(nameof(UsuarioEmailTamanhoMaximo), argument);
        public string UsuarioEmailInvalido => ObterResource(nameof(UsuarioEmailInvalido));

        public string UsuarioImagemObrigatoria => ObterResource(nameof(UsuarioImagemObrigatoria));
        public string UsuarioNickNameInvalido => ObterResource(nameof(UsuarioNickNameInvalido));
        public string UsuarioNickNameObrigatorio => ObterResource(nameof(UsuarioNickNameObrigatorio));
        public string UsuarioNickNameTamanhoMaximo(int argument) => ObterResource(nameof(UsuarioNickNameTamanhoMaximo), argument);


    }

}
