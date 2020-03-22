namespace LevelLearn.ViewModel.Usuarios
{
    public class UsuarioVM
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string NickName { get; set; }
        public string ImagemUrl { get; set; }
        public TokenVM Token { get; set; }
    }
}
