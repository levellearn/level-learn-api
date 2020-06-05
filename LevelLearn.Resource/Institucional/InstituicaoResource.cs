namespace LevelLearn.Resource.Institucional
{
    public class InstituicaoResource : ResourceBase
    {
        public InstituicaoResource() : base(typeof(InstituicaoResource))
        {
        }

        public string InstituicaoNaoEncontrada => ObterResource(nameof(InstituicaoNaoEncontrada));
        public string InstituicaoNaoPermitida => ObterResource(nameof(InstituicaoNaoPermitida));
        public string InstituicaoJaExiste => ObterResource(nameof(InstituicaoJaExiste));
        public string InstituicaoNomeObrigatorio => ObterResource(nameof(InstituicaoNomeObrigatorio));

        public string InstituicaoNomeTamanho(params object[] replacements)
        {
            return ObterResource(nameof(InstituicaoNomeTamanho), replacements);
        }

        public string InstituicaoDescricaoObrigatorio => ObterResource(nameof(InstituicaoDescricaoObrigatorio));

        public string InstituicaoDescricaoTamanho(params object[] replacements)
        {
            return ObterResource(nameof(InstituicaoDescricaoTamanho), replacements);
        }


    }

}
