using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum TipoPessoa
    {
        [Description("Vazio")]
        Vazio = 0,
        [Description("Admin Sistema")]
        Admin = 1,
        [Description("Professor")]
        Professor = 2,
        [Description("Aluno")]
        Aluno = 3
    }
}
