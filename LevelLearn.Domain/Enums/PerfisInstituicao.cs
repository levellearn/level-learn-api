using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum PerfisInstituicao
    {
        [Description("Vazio")]
        Vazio = 0,
        [Description("Professor Admin")]
        ProfessorAdmin = 1,
        [Description("Professor")]
        Professor = 2,
        [Description("Aluno")]
        Aluno = 3
    }
}
