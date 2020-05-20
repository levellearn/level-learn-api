namespace LevelLearn.Resource.Institucional
{
    public class CursoResource : ResourceBase
    {
        public CursoResource()
                    : base(typeof(CursoResource))
        {
        }

        public string CursoNaoEncontrado => GetResource(nameof(CursoNaoEncontrado));
        public string CursoNaoPermitido => GetResource(nameof(CursoNaoPermitido));
        public string CursoJaExiste => GetResource(nameof(CursoJaExiste));
        public string IdObrigatorio => GetResource(nameof(IdObrigatorio));
        public string CursoNomeObrigatorio => GetResource(nameof(CursoNomeObrigatorio));

        public string CursoNomeTamanho(params object[] arguments)
        {
            return GetResource(nameof(CursoNomeTamanho), arguments);
        }

        public string CursoDescricaoObrigatorio => GetResource(nameof(CursoDescricaoObrigatorio));

        public string CursoDescricaoTamanho(params object[] arguments)
        {
            return GetResource(nameof(CursoDescricaoTamanho), arguments);
        }

        public string CursoSiglaObrigatorio => GetResource(nameof(CursoSiglaObrigatorio));

        public string CursoSiglaTamanho(params object[] arguments)
        {
            return GetResource(nameof(CursoSiglaTamanho), arguments);
        }

    }

}
