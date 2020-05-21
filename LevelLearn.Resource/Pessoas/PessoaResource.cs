namespace LevelLearn.Resource
{
    public class PessoaResource : ResourceBase
    {
        public PessoaResource() : base(typeof(PessoaResource))
        { }

        public string IdObrigatorio => GetResource(nameof(IdObrigatorio));
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

    }

}
