namespace LevelLearn.Resource.Institucional
{
    public class TurmaResource : ResourceBase
    {
        public TurmaResource() : base(typeof(TurmaResource))
        {
        }

        public string TurmaNaoEncontrada => ObterResource(nameof(TurmaNaoEncontrada));
        public string TurmaNaoPermitida => ObterResource(nameof(TurmaNaoPermitida));
        public string TurmaJaExiste => ObterResource(nameof(TurmaJaExiste));

        public string TurmaNomeObrigatorio => ObterResource(nameof(TurmaNomeDisciplinaObrigatorio));
        public string TurmaNomeTamanho(params object[] arguments) => ObterResource(nameof(TurmaNomeTamanho), arguments);

        public string TurmaNomeDisciplinaObrigatorio => ObterResource(nameof(TurmaNomeDisciplinaObrigatorio));
        public string TurmaNomeDisciplinaTamanho(params object[] arguments) => ObterResource(nameof(TurmaNomeDisciplinaTamanho), arguments);

        public string TurmaDescricaoObrigatoria => ObterResource(nameof(TurmaDescricaoObrigatoria));
        public string TurmaDescricaoTamanhoMaximo(int tamanho) => ObterResource(nameof(TurmaDescricaoTamanhoMaximo), tamanho);
       

    }

}
