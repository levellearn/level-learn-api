namespace LevelLearn.Domain.Validators.RegrasAtributos
{
    public static class RegraUsuario
    {
        public const int NICKNAME_TAMANHO_MAX = 30;
        public const int EMAIL_TAMANHO_MAX = 190;
        public const int SENHA_TAMANHO_MIN = 6;
        public const int SENHA_TAMANHO_MAX = 100;
        public const bool SENHA_REQUER_DIGITO = false;
        public const bool SENHA_REQUER_MINUSCULO = true;
        public const bool SENHA_REQUER_MAIUSCULO = false;
        public const bool SENHA_REQUER_ESPECIAL = false;
    }
}
