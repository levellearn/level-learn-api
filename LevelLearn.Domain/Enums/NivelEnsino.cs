using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum NivelEnsino
    {
        [Description("Outros")]
        Outros = 0,
        [Description("Ensino Básico")]
        EnsinoBasico,
        [Description("Superior")]
        Superior
    }
}
