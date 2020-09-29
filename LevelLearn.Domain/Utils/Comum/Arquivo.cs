using System.IO;

namespace LevelLearn.Domain.Utils.Comum
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Extensao { get; set; }
        public string Tamanho { get; set; }
        public Stream File { get; set; }
    }
}
