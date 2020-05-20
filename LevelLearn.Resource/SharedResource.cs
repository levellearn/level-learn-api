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

        #region Pessoa
        public string PessoaCPFJaExiste => GetResource(nameof(PessoaCPFJaExiste));
        public string PessoaDataNascimentoInvalida => GetResource(nameof(PessoaDataNascimentoInvalida));
        public string PessoaGeneroObrigatorio => GetResource(nameof(PessoaGeneroObrigatorio));
        public string PessoaNomeObrigatorio => GetResource(nameof(PessoaNomeObrigatorio));
        public string PessoaNomePrecisaSobrenome => GetResource(nameof(PessoaNomePrecisaSobrenome));
        public string PessoaNomeTamanho(params object[] arguments)
        {
            return GetResource(nameof(PessoaNomeTamanho), arguments);
        }
        public string PessoaTipoPessoaInvalido => GetResource(nameof(PessoaTipoPessoaInvalido));
        public string PessoaCelularInvalido => GetResource(nameof(PessoaCelularInvalido));
        public string PessoaCPFInvalido => GetResource(nameof(PessoaCPFInvalido));
        public string AlunoRAObrigatorio => GetResource(nameof(AlunoRAObrigatorio));
        public string ProfessorCPFObrigatorio => GetResource(nameof(ProfessorCPFObrigatorio));
        #endregion

    }

}
