namespace LevelLearn.ViewModel.Usuarios
{
    public class LoginUsuarioVM //: AbstractValidator<ApplicationUser>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string RefreshToken { get; set; }
        public TipoAutenticacao TipoAutenticacao { get; set; }
    }
}
