namespace LevelLearn.Domain.Validators
{
    public static class PropertiesConfig
    {   

        public static class Pessoa
        {
            public const int NOME_TAMANHO_MIN = 3;
            public const int NOME_TAMANHO_MAX = 150;
            public const int USERNAME_TAMANHO_MAX = 20;
            public const int CELULAR_TAMANHO = 14;
            public const int EMAIL_TAMANHO_MAX = 190;
            public const int CPF_TAMANHO = 11;
        }

        public static class Instituicao
        {
            public const int NOME_TAMANHO_MIN = 3;
            public const int NOME_TAMANHO_MAX = 100;
            public const int DESCRICAO_TAMANHO_MAX = 2_000;
        }

        public static class Curso
        {
            public const int NOME_TAMANHO_MIN = 3;
            public const int NOME_TAMANHO_MAX = 100;
            public const int SIGLA_TAMANHO_MIN = 2;
            public const int SIGLA_TAMANHO_MAX = 20;
            public const int DESCRICAO_TAMANHO_MAX = 2_000;
        }


    }

    


}
