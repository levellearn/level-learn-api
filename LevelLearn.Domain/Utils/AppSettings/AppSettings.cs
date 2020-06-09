namespace LevelLearn.Domain.Utils.AppSettings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JWTSettings JWTSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
        public ApiSettings ApiSettings { get; set; }
        public FirebaseSettings FirebaseSettings { get; set; }
    }

}
