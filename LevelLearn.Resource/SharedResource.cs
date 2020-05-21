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


        public string ObterResource(string resourceKey)
        {
            return _localizer?[resourceKey]?.Value ?? resourceKey;
        }

        public string ObterResource(string resourceKey, params object[] arguments)
        {
            return _localizer?[resourceKey, arguments]?.Value ?? resourceKey;
        }

        public string ErroInternoServidor => ObterResource(nameof(ErroInternoServidor));
        public string CadastradoSucesso => ObterResource(nameof(CadastradoSucesso));
        public string AtualizadoSucesso => ObterResource(nameof(AtualizadoSucesso));
        public string DeletadoSucesso => ObterResource(nameof(DeletadoSucesso));
        public string DadosInvalidos => ObterResource(nameof(DadosInvalidos));
        public string NaoEncontrado => ObterResource(nameof(NaoEncontrado));
        public string FalhaCadastrar => ObterResource(nameof(FalhaCadastrar));
        public string FalhaAtualizar => ObterResource(nameof(FalhaAtualizar));
        public string FalhaDeletar => ObterResource(nameof(FalhaDeletar));
        public string IdObrigatorio => ObterResource(nameof(IdObrigatorio));




    }

}
