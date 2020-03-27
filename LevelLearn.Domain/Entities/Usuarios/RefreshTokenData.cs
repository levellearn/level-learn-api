namespace LevelLearn.Domain.Entities.Usuarios
{
    public class RefreshTokenData
    {
        public RefreshTokenData() { }

        public RefreshTokenData(string refreshToken, string userName)
        {
            RefreshToken = refreshToken;
            UserName = userName;
        }

        public string RefreshToken { get; set; }
        public string UserName { get; set; }
    }
}
