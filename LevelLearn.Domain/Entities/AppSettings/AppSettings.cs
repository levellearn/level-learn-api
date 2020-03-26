namespace LevelLearn.Domain.Entities.AppSettings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JWTSettings JWTSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
    }

    public class ConnectionStrings
    {
        public string LevelLearnSQLServer { get; set; }
        public string LogDB { get; set; }
        public string RedisCache { get; set; }
    }

    public class JWTSettings
    {
        public string ChavePrivada { get; set; }
        public int ExpiracaoSegundos { get; set; }
        public int RefreshTokenExpiracaoSegundos { get; set; }
        public int TempoToleranciaSegundos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }

    public class IdentitySettings
    {
        public int TempoBloqueioMinutos { get; set; }
        public int TentativaMaximaAcesso { get; set; }
        public int TamanhoMinimoSenha { get; set; }
    }

}
