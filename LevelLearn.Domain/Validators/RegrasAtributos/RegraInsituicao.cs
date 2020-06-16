namespace LevelLearn.Domain.Validators.RegrasAtributos
{
    public static class RegraInsituicao
    {
        public const int NOME_TAMANHO_MIN = 3;
        public const int NOME_TAMANHO_MAX = 100;
        public const int SIGLA_TAMANHO_MIN = 2;
        public const int SIGLA_TAMANHO_MAX = 20;
        public const int DESCRICAO_TAMANHO_MAX = 2_000;
        public const int CEP_TAMANHO = 8;
        public const int MUNICIPIO_TAMANHO_MIN = 3;
        public const int MUNICIPIO_TAMANHO_MAX = 100;
        public const int UF_TAMANHO = 2;
    }
}
