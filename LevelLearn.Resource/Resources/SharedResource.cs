using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource
{
    public interface ISharedResource
    {
        public string GetValue(string resourceKey);
        public string GetValue(string resourceKey, params object[] arguments);


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

        #region Curso
        public string CursoNaoEncontrado { get; }
        public string CursoNaoPermitido { get; }
        public string CursoJaExiste { get; }
        public string CursoNomeObrigatorio { get; }
        public string CursoNomeTamanho(params object[] arguments);
        public string CursoDescricaoObrigatorio { get; }
        public string CursoDescricaoTamanho(params object[] arguments);
        public string CursoSiglaObrigatorio { get; }
        public string CursoSiglaTamanho(params object[] arguments);
        #endregion

    }

    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource() { }

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string GetValue(string resourceKey)
        {
            return _localizer?[resourceKey]?.Value ?? resourceKey;
        }

        public string GetValue(string resourceKey, params object[] arguments)
        {
            return _localizer?[resourceKey, arguments]?.Value ?? resourceKey;
        }

        #region Geral
        public string ErroInternoServidor => GetValue(nameof(ErroInternoServidor));
        public string CadastradoSucesso => GetValue(nameof(CadastradoSucesso));
        public string AtualizadoSucesso => GetValue(nameof(AtualizadoSucesso));
        public string DeletadoSucesso => GetValue(nameof(DeletadoSucesso));
        public string DadosInvalidos => GetValue(nameof(DadosInvalidos));
        public string NaoEncontrado => GetValue(nameof(NaoEncontrado));
        public string FalhaCadastrar => GetValue(nameof(FalhaCadastrar));
        public string FalhaAtualizar => GetValue(nameof(FalhaAtualizar));
        public string FalhaDeletar => GetValue(nameof(FalhaDeletar));
        public string IdObrigatorio => GetValue(nameof(IdObrigatorio));

        #endregion

        #region Usuario
        public string UsuarioLoginSucesso => GetValue(nameof(UsuarioLoginSucesso));
        public string UsuarioLogoutSucesso => GetValue(nameof(UsuarioLogoutSucesso));
        public string UsuarioEmailJaExiste => GetValue(nameof(UsuarioEmailJaExiste));
        public string UsuarioContaBloqueada => GetValue(nameof(UsuarioContaBloqueada));
        public string UsuarioEmailNaoConfirmado => GetValue(nameof(UsuarioEmailNaoConfirmado));
        public string UsuarioEmailConfirmarFalha => GetValue(nameof(UsuarioEmailConfirmarFalha));
        public string UsuarioEmailConfirmarSucesso => GetValue(nameof(UsuarioEmailConfirmarSucesso));
        public string UsuarioLoginFalha => GetValue(nameof(UsuarioLoginFalha)); 
        public string UsuarioSenhaObrigatoria => GetValue(nameof(UsuarioSenhaObrigatoria)); 
        public string UsuarioSenhaTamanho(params object[] arguments)
        {
            return GetValue(nameof(UsuarioSenhaTamanho), arguments);
        }
        public string UsuarioConfirmacaoSenhaObrigatoria => GetValue(nameof(UsuarioConfirmacaoSenhaObrigatoria)); 
        public string UsuarioConfirmacaoSenhaNaoConfere => GetValue(nameof(UsuarioConfirmacaoSenhaNaoConfere));
        public string UsuarioSenhaRequerMaiusculo => GetValue(nameof(UsuarioSenhaRequerMaiusculo));
        public string UsuarioSenhaRequerMinusculo => GetValue(nameof(UsuarioSenhaRequerMinusculo));
        public string UsuarioSenhaRequerDigito => GetValue(nameof(UsuarioSenhaRequerDigito));
        public string UsuarioSenhaRequerEspecial => GetValue(nameof(UsuarioSenhaRequerEspecial));
        public string UsuarioEmailObrigatorio => GetValue(nameof(UsuarioEmailObrigatorio));
        public string UsuarioEmailTamanhoMaximo(int argument)
        {
           return GetValue(nameof(UsuarioEmailTamanhoMaximo), argument);
        }
        public string UsuarioEmailInvalido => GetValue(nameof(UsuarioEmailInvalido));
        #endregion

        #region Pessoa
        public string PessoaCPFJaExiste => GetValue(nameof(PessoaCPFJaExiste));
        public string PessoaDataNascimentoInvalida => GetValue(nameof(PessoaDataNascimentoInvalida));
        public string PessoaGeneroObrigatorio => GetValue(nameof(PessoaGeneroObrigatorio));
        public string PessoaImagemObrigatoria => GetValue(nameof(PessoaImagemObrigatoria));
        public string PessoaNickNameInvalido => GetValue(nameof(PessoaNickNameInvalido));
        public string PessoaNickNameObrigatorio => GetValue(nameof(PessoaNickNameObrigatorio));
        public string PessoaNickNameTamanhoMaximo(int argument)
        {
            return GetValue(nameof(PessoaNickNameTamanhoMaximo), argument);
        }
        public string PessoaNomeObrigatorio => GetValue(nameof(PessoaNomeObrigatorio));
        public string PessoaNomePrecisaSobrenome => GetValue(nameof(PessoaNomePrecisaSobrenome));
        public string PessoaNomeTamanho(params object[] arguments)
        {
            return GetValue(nameof(UsuarioSenhaTamanho), arguments);
        }
        public string PessoaTipoPessoaInvalido => GetValue(nameof(PessoaTipoPessoaInvalido));
        public string PessoaCelularInvalido => GetValue(nameof(PessoaCelularInvalido));
        public string PessoaCPFInvalido => GetValue(nameof(PessoaCPFInvalido));
        public string AlunoRAObrigatorio => GetValue(nameof(AlunoRAObrigatorio));
        public string ProfessorCPFObrigatorio => GetValue(nameof(ProfessorCPFObrigatorio));
        #endregion

        #region Curso
        public string CursoNaoEncontrado => GetValue(nameof(CursoNaoEncontrado));
        public string CursoNaoPermitido => GetValue(nameof(CursoNaoPermitido));
        public string CursoJaExiste => GetValue(nameof(CursoJaExiste));
        public string CursoNomeObrigatorio => GetValue(nameof(CursoNomeObrigatorio));
        public string CursoNomeTamanho(params object[] arguments)
        {
            return GetValue(nameof(CursoNomeTamanho), arguments);
        }
        public string CursoDescricaoObrigatorio => GetValue(nameof(CursoDescricaoObrigatorio));
        public string CursoDescricaoTamanho(params object[] arguments)
        {
            return GetValue(nameof(CursoDescricaoTamanho), arguments);
        }
        public string CursoSiglaObrigatorio => GetValue(nameof(CursoSiglaObrigatorio));
        public string CursoSiglaTamanho(params object[] arguments)
        {
            return GetValue(nameof(CursoSiglaTamanho), arguments);
        }
        #endregion

    }

}
