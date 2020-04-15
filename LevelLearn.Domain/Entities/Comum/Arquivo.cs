using Microsoft.AspNetCore.Http;

namespace LevelLearn.Domain.Entities.Comum
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Extensao { get; set; }
        public string Tamanho { get; set; }
        public IFormFile File { get; set; }
    }
}
