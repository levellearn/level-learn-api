using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource.Usuarios
{
    public class UsuarioResource : ResourceBase
    {
        public UsuarioResource() : base(typeof(UsuarioResource))
        {
        }

        public string IdObrigatorio => GetResource(nameof(IdObrigatorio));
        public string UsuarioLoginSucesso => GetResource(nameof(UsuarioLoginSucesso));
        public string UsuarioLogoutSucesso => GetResource(nameof(UsuarioLogoutSucesso));
        public string UsuarioEmailJaExiste => GetResource(nameof(UsuarioEmailJaExiste));
        public string UsuarioContaBloqueada => GetResource(nameof(UsuarioContaBloqueada));
        public string UsuarioEmailNaoConfirmado => GetResource(nameof(UsuarioEmailNaoConfirmado));
        public string UsuarioEmailConfirmarFalha => GetResource(nameof(UsuarioEmailConfirmarFalha));
        public string UsuarioEmailConfirmarSucesso => GetResource(nameof(UsuarioEmailConfirmarSucesso));
        public string UsuarioLoginFalha => GetResource(nameof(UsuarioLoginFalha)); 

        public string UsuarioSenhaObrigatoria => GetResource(nameof(UsuarioSenhaObrigatoria)); 
        public string UsuarioSenhaTamanho(params object[] arguments)
        {
            return GetResource(nameof(UsuarioSenhaTamanho), arguments);
        }
        public string UsuarioConfirmacaoSenhaObrigatoria => GetResource(nameof(UsuarioConfirmacaoSenhaObrigatoria)); 
        public string UsuarioConfirmacaoSenhaNaoConfere => GetResource(nameof(UsuarioConfirmacaoSenhaNaoConfere));
        public string UsuarioSenhaRequerMaiusculo => GetResource(nameof(UsuarioSenhaRequerMaiusculo));
        public string UsuarioSenhaRequerMinusculo => GetResource(nameof(UsuarioSenhaRequerMinusculo));
        public string UsuarioSenhaRequerDigito => GetResource(nameof(UsuarioSenhaRequerDigito));
        public string UsuarioSenhaRequerEspecial => GetResource(nameof(UsuarioSenhaRequerEspecial));

        public string UsuarioEmailObrigatorio => GetResource(nameof(UsuarioEmailObrigatorio));
        public string UsuarioEmailTamanhoMaximo(int argument)
        {
           return GetResource(nameof(UsuarioEmailTamanhoMaximo), argument);
        }
        public string UsuarioEmailInvalido => GetResource(nameof(UsuarioEmailInvalido));


        public string UsuarioImagemObrigatoria => GetResource(nameof(UsuarioImagemObrigatoria));
        public string UsuarioNickNameInvalido => GetResource(nameof(UsuarioNickNameInvalido));
        public string UsuarioNickNameObrigatorio => GetResource(nameof(UsuarioNickNameObrigatorio));
        public string UsuarioNickNameTamanhoMaximo(int argument)
        {
            return GetResource(nameof(UsuarioNickNameTamanhoMaximo), argument);
        }


    }

}
