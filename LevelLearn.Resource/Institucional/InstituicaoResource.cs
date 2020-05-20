namespace LevelLearn.Resource.Institucional
{
    public class InstituicaoResource : ResourceBase
    {
        public InstituicaoResource() 
            : base(typeof(InstituicaoResource))
        {  
        }       

        public string IdObrigatorio => GetResource(nameof(IdObrigatorio));
        public string InstituicaoNaoEncontrada => GetResource(nameof(InstituicaoNaoEncontrada));
        public string InstituicaoNaoPermitida => GetResource(nameof(InstituicaoNaoPermitida));
        public string InstituicaoJaExiste => GetResource(nameof(InstituicaoJaExiste));
        public string InstituicaoNomeObrigatorio => GetResource(nameof(InstituicaoNomeObrigatorio));

        public string InstituicaoNomeTamanho(params object[] replacements)
        {
            return GetResource(nameof(InstituicaoNomeTamanho), replacements);
        }

        public string InstituicaoDescricaoObrigatorio => GetResource(nameof(InstituicaoDescricaoObrigatorio));

        public string InstituicaoDescricaoTamanho(params object[] replacements)
        {
            return GetResource(nameof(InstituicaoDescricaoTamanho), replacements);
        }


    }

}
