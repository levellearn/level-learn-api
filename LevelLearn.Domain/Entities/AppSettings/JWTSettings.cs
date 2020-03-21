namespace LevelLearn.Domain.Entities.AppSettings
{
    public class JWTSettings
    {
        public string ChavePrivada { get; set; }
        public int ExpiracaoMinutos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
