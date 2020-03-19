using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum Generos
    {
        [Description("Nenhum")]
        Nenhum = 0,
        [Description("Masculino")]
        Masculino,
        [Description("Feminino")]
        Feminino,
        [Description("Outros")]
        Outros
    }
}
