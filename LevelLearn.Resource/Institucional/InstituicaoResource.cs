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
        public string InstituicaoDescricaoTamanho(params object[] replacements) => ObterResource(nameof(InstituicaoDescricaoTamanho), replacements);

        public string InstituicaoCNPJInvalido => ObterResource(nameof(InstituicaoCNPJInvalido));

        public string InstituicaoSiglaObrigatorio => ObterResource(nameof(InstituicaoSiglaObrigatorio));
        public string InstituicaoSiglaTamanho(params object[] replacements) => ObterResource(nameof(InstituicaoSiglaTamanho), replacements);      

        public string InstituicaoCEPObrigatorio => ObterResource(nameof(InstituicaoCEPObrigatorio));
        public string InstituicaoCEPTamanhoMaximo(int argument) => ObterResource(nameof(InstituicaoCEPTamanhoMaximo), argument);        

        public string InstituicaoMunicipioObrigatorio => ObterResource(nameof(InstituicaoMunicipioObrigatorio));
        public string InstituicaoMunicipioTamanho(params object[] replacements) => ObterResource(nameof(InstituicaoMunicipioTamanho), replacements);

        public string InstituicaoUFObrigatorio => ObterResource(nameof(InstituicaoUFObrigatorio));
        public string InstituicaoUFTamanhoMaximo(int argument) => ObterResource(nameof(InstituicaoUFTamanhoMaximo), argument);

    }

}
