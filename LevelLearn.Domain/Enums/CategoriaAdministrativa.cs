using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum CategoriaAdministrativa
    {
        [Description("Privada")]
        Privada,
        [Description("Federal")]
        Federal,
        [Description("Estadual")]
        Estadual,
        [Description("Municipal")]
        Municipal,
        [Description("Outros")]
        Outros
    }
}
