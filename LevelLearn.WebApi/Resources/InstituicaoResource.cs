using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Resources;

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
namespace LevelLearn.WebApi.Resources
{
    public class InstituicaoResource : ResourceBase
    {
        //private readonly IStringLocalizer _localizer;

        //public SharedResource() { }

        //public SharedResource(IStringLocalizer<SharedResource> localizer)
        //{
        //    _localizer = localizer;
        //}

        //public string GetValue(string resourceKey)
        //{
        //    return _localizer?[resourceKey]?.Value ?? resourceKey;
        //}

        //public string GetValue(string resourceKey, params object[] arguments)
        //{
        //    return _localizer?[resourceKey, arguments]?.Value ?? resourceKey;
        //}

        //private readonly ResourceManager _resourceManager;

        public InstituicaoResource() : base(typeof(InstituicaoResource))
        {
            //_resourceManager = new ResourceManager("LevelLearn.Resources.InstituicaoResource", typeof(WebApi.Startup).GetTypeInfo().Assembly);
            //_resourceManager = new ResourceManager(typeof(InstituicaoResource));
        }

        /// <summary>
        /// Builds up a string looking up a resource and doing the replacements.
        /// </summary>
        /// <param name="resourceStringName">Name of resource to use</param>
        /// <param name="replacements">Strings to use for replacing in the resource string</param>
        private string BuildStringFromResource(string resourceStringName, params object[] replacements)
        {
            return string.Format(_resourceManager.GetString(resourceStringName), replacements);
        }

        public string InstituicaoNaoEncontrada => BuildStringFromResource(nameof(InstituicaoNaoEncontrada));
        public string InstituicaoNaoPermitida => BuildStringFromResource(nameof(InstituicaoNaoPermitida));
        public string InstituicaoJaExiste => BuildStringFromResource(nameof(InstituicaoJaExiste));
        public string InstituicaoNomeObrigatorio => BuildStringFromResource(nameof(InstituicaoNomeObrigatorio));
        public string InstituicaoNomeTamanho(params object[] arguments)
        {
            return BuildStringFromResource(nameof(InstituicaoNomeTamanho), arguments);
        }
        public string InstituicaoDescricaoObrigatorio => BuildStringFromResource(nameof(InstituicaoDescricaoObrigatorio));
        public string InstituicaoDescricaoTamanho(params object[] arguments)
        {
            return BuildStringFromResource(nameof(InstituicaoDescricaoTamanho), arguments);
        }



    }

}
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
