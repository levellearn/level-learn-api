using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource
{
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource() { }

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }


        public string GetResource(string resourceKey)
        {
            return _localizer?[resourceKey]?.Value ?? resourceKey;
        }

        public string GetResource(string resourceKey, params object[] arguments)
        {
            return _localizer?[resourceKey, arguments]?.Value ?? resourceKey;
        }

        #region Geral
        public string ErroInternoServidor => GetResource(nameof(ErroInternoServidor));
        public string CadastradoSucesso => GetResource(nameof(CadastradoSucesso));
        public string AtualizadoSucesso => GetResource(nameof(AtualizadoSucesso));
        public string DeletadoSucesso => GetResource(nameof(DeletadoSucesso));
        public string DadosInvalidos => GetResource(nameof(DadosInvalidos));
        public string NaoEncontrado => GetResource(nameof(NaoEncontrado));
        public string FalhaCadastrar => GetResource(nameof(FalhaCadastrar));
        public string FalhaAtualizar => GetResource(nameof(FalhaAtualizar));
        public string FalhaDeletar => GetResource(nameof(FalhaDeletar));
        public string IdObrigatorio => GetResource(nameof(IdObrigatorio));

        #endregion

        #region Usuario
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
        #endregion

        #region Pessoa
        public string PessoaCPFJaExiste => GetResource(nameof(PessoaCPFJaExiste));
        public string PessoaDataNascimentoInvalida => GetResource(nameof(PessoaDataNascimentoInvalida));
        public string PessoaGeneroObrigatorio => GetResource(nameof(PessoaGeneroObrigatorio));
        public string PessoaImagemObrigatoria => GetResource(nameof(PessoaImagemObrigatoria));
        public string PessoaNickNameInvalido => GetResource(nameof(PessoaNickNameInvalido));
        public string PessoaNickNameObrigatorio => GetResource(nameof(PessoaNickNameObrigatorio));
        public string PessoaNickNameTamanhoMaximo(int argument)
        {
            return GetResource(nameof(PessoaNickNameTamanhoMaximo), argument);
        }
        public string PessoaNomeObrigatorio => GetResource(nameof(PessoaNomeObrigatorio));
        public string PessoaNomePrecisaSobrenome => GetResource(nameof(PessoaNomePrecisaSobrenome));
        public string PessoaNomeTamanho(params object[] arguments)
        {
            return GetResource(nameof(UsuarioSenhaTamanho), arguments);
        }
        public string PessoaTipoPessoaInvalido => GetResource(nameof(PessoaTipoPessoaInvalido));
        public string PessoaCelularInvalido => GetResource(nameof(PessoaCelularInvalido));
        public string PessoaCPFInvalido => GetResource(nameof(PessoaCPFInvalido));
        public string AlunoRAObrigatorio => GetResource(nameof(AlunoRAObrigatorio));
        public string ProfessorCPFObrigatorio => GetResource(nameof(ProfessorCPFObrigatorio));
        #endregion

    }

}
