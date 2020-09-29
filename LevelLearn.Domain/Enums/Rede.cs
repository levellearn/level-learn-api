using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum Rede
    {
        [Description("Outros")]
        Outros = 0,
        [Description("Pública")]
        Publica,
        [Description("Privada")]
        Privada
    }
}
