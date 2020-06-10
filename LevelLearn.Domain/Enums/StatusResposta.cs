using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum StatusResposta
    {
        [Description("Pedente")]
        Pedente = 1,
        [Description("Respondida")]
        Respondida = 2,
        [Description("Corrigida")]
        Corrigida = 3
    }
}
