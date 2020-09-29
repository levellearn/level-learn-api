using System;

namespace LevelLearn.ViewModel.Usuarios
{
    public class TokenVM
    {
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
