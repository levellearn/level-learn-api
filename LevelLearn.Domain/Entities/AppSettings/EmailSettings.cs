namespace LevelLearn.Domain.Entities.AppSettings
{
    public class EmailSettings
    {
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Enviar { get; set; }
    }


}
