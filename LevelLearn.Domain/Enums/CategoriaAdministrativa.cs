using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum CategoriaAdministrativa
    {
        [Description("Outros")]
        Outros,
        [Description("Privada")]
        Privada,
        [Description("Federal")]
        Federal,
        [Description("Estadual")]
        Estadual,
        [Description("Municipal")]
        Municipal
    }
}
