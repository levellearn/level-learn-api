using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum TipoPresenca
    {
        [Description("Presente")]
        Presente = 1,
        [Description("Atrasado")]
        Atrasado = 2,
        [Description("Faltou")]
        Faltou = 3
    }
}
