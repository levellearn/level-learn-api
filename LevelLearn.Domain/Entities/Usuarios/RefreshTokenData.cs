namespace LevelLearn.Domain.Entities.Usuarios
{
    /// <summary>
    /// Armazena o refresh token e o email único do usuário para salvar no BD de Cache
    /// </summary>
    public class RefreshTokenData
    {
        public RefreshTokenData() { }

        public RefreshTokenData(string refreshToken, string email)
        {
            RefreshToken = refreshToken;
            Email = email;
        }

        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
