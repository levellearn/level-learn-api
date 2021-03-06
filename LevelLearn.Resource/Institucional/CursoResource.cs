﻿namespace LevelLearn.Resource.Institucional
{
    public class CursoResource : ResourceBase
    {
        public CursoResource() : base(typeof(CursoResource))
        {
        }

        public string CursoNaoEncontrado => ObterResource(nameof(CursoNaoEncontrado));
        public string CursoNaoPermitido => ObterResource(nameof(CursoNaoPermitido));
        public string CursoJaExiste => ObterResource(nameof(CursoJaExiste));

        public string CursoNomeObrigatorio => ObterResource(nameof(CursoNomeObrigatorio));
        public string CursoNomeTamanho(params object[] arguments) => ObterResource(nameof(CursoNomeTamanho), arguments);

        public string CursoDescricaoObrigatorio => ObterResource(nameof(CursoDescricaoObrigatorio));
        public string CursoDescricaoTamanho(params object[] arguments) => ObterResource(nameof(CursoDescricaoTamanho), arguments);

        public string CursoSiglaObrigatorio => ObterResource(nameof(CursoSiglaObrigatorio));
        public string CursoSiglaTamanho(params object[] arguments) => ObterResource(nameof(CursoSiglaTamanho), arguments);

    }

}
