using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum TiposPessoa
    {
        [Description("Vazio")]
        Vazio = 0,
        [Description("Admin sistema")]
        Admin = 1,
        [Description("Professor")]
        Professor = 2,
        [Description("Aluno")]
        Aluno = 3
    }
}
