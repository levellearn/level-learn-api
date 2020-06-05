namespace LevelLearn.Resource
{
    public class PessoaResource : ResourceBase
    {
        private static PessoaResource _instancia;

        private PessoaResource() : base(typeof(PessoaResource))
        { }

        /// <summary>
        /// Obtém uma instância única de PessoaResource (Singleton)
        /// </summary>
        /// <returns></returns>
        public static PessoaResource ObterInstancia()
        {
            if (_instancia == null)
                _instancia = new PessoaResource();

            return _instancia;
        }

        public string PessoaCPFJaExiste => ObterResource(nameof(PessoaCPFJaExiste));
        public string PessoaDataNascimentoInvalida => ObterResource(nameof(PessoaDataNascimentoInvalida));
        public string PessoaGeneroObrigatorio => ObterResource(nameof(PessoaGeneroObrigatorio));
        public string PessoaNomeObrigatorio => ObterResource(nameof(PessoaNomeObrigatorio));
        public string PessoaNomePrecisaSobrenome => ObterResource(nameof(PessoaNomePrecisaSobrenome));

        public string PessoaNomeTamanho(params object[] arguments)
        {
            return ObterResource(nameof(PessoaNomeTamanho), arguments);
        }
        
        public string PessoaTipoPessoaInvalido => ObterResource(nameof(PessoaTipoPessoaInvalido));
        public string PessoaCelularInvalido => ObterResource(nameof(PessoaCelularInvalido));
        public string PessoaCPFInvalido => ObterResource(nameof(PessoaCPFInvalido));
        public string AlunoRAObrigatorio => ObterResource(nameof(AlunoRAObrigatorio));
        public string ProfessorCPFObrigatorio => ObterResource(nameof(ProfessorCPFObrigatorio));
       
    }

}
