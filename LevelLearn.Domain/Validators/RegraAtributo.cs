﻿namespace LevelLearn.Domain.Validators
{
    public static class RegraAtributo
    {
        public static class Pessoa
        {
            public const int NOME_TAMANHO_MIN = 3;
            public const int NOME_TAMANHO_MAX = 150;
            public const int NICKNAME_TAMANHO_MAX = 30;
            public const int CELULAR_TAMANHO = 14;
            public const int EMAIL_TAMANHO_MAX = 190;
            public const int CPF_TAMANHO = 11;

            public const int SENHA_TAMANHO_MIN = 6;
            public const int SENHA_TAMANHO_MAX = 100;
            public const bool SENHA_REQUER_DIGITO = false;
            public const bool SENHA_REQUER_MINUSCULO = true;
            public const bool SENHA_REQUER_MAIUSCULO = false;
            public const bool SENHA_REQUER_ESPECIAL = false;
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

        public static class Turma
        {
            public const int NOME_TAMANHO_MIN = 3;
            public const int NOME_TAMANHO_MAX = 100;
            public const int NOME_DISCIPLINA_TAMANHO_MIN = 3;
            public const int NOME_DISCIPLINA_TAMANHO_MAX = 100;
            public const int DESCRICAO_TAMANHO_MAX = 2_000;
        }


    }

    


}