﻿using Microsoft.Extensions.Localization;

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
        #endregion

        #region Instituicao
        public string InstituicaoNomeObrigatorio { get; }
        public string InstituicaoNomeTamanho(params object[] arguments);
        public string InstituicaoDescricaoObrigatorio { get; }
        public string InstituicaoDescricaoTamanho(params object[] arguments);
        public string InstituicaoNaoEncontrada { get; }
        public string InstituicaoNaoPermitida { get; }
        public string InstituicaoJaExiste { get; }
        #endregion

        #region Usuario
        public string LoginSucesso { get; }
        public string LogoutSucesso { get; }
        public string EmailJaExiste { get; }
        public string CPFJaExiste { get; }
        public string ContaBloqueada { get; }
        public string LoginFalha { get; }
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

    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource() { }

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
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

        #endregion

        #region Instituicao
        public string InstituicaoNaoEncontrada => GetValue(nameof(InstituicaoNaoEncontrada));
        public string InstituicaoNaoPermitida => GetValue(nameof(InstituicaoNaoPermitida));
        public string InstituicaoJaExiste => GetValue(nameof(InstituicaoJaExiste));
        public string InstituicaoNomeObrigatorio => GetValue(nameof(InstituicaoNomeObrigatorio));
        public string InstituicaoNomeTamanho(params object[] arguments)
        {
            return GetValue(nameof(InstituicaoNomeTamanho), arguments);
        }
        public string InstituicaoDescricaoObrigatorio => GetValue(nameof(InstituicaoDescricaoObrigatorio));
        public string InstituicaoDescricaoTamanho(params object[] arguments)
        {
            return GetValue(nameof(InstituicaoDescricaoTamanho), arguments);
        }
        #endregion

        #region Usuario
        public string LoginSucesso => GetValue(nameof(LoginSucesso));
        public string LogoutSucesso => GetValue(nameof(LogoutSucesso));
        public string EmailJaExiste => GetValue(nameof(EmailJaExiste));
        public string CPFJaExiste => GetValue(nameof(CPFJaExiste));
        public string ContaBloqueada => GetValue(nameof(ContaBloqueada));
        public string LoginFalha => GetValue(nameof(LoginFalha)); 
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


        public string GetValue(string resourceKey)
        {
            return _localizer?[resourceKey]?.Value ?? resourceKey;
        }

        public string GetValue(string resourceKey, params object[] arguments)
        {
            return _localizer?[resourceKey, arguments]?.Value ?? resourceKey;
        }
        
    }

}
