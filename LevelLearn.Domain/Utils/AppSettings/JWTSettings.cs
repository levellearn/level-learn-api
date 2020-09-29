namespace LevelLearn.Domain.Utils.AppSettings
{
    public class JWTSettings
    {
        public string ChavePrivada { get; set; }
        public int ExpiracaoSegundos { get; set; }
        public int RefreshTokenExpiracaoSegundos { get; set; }
        public int TempoToleranciaSegundos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }


}
