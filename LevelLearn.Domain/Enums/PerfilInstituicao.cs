using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum PerfilInstituicao
    {
        [Description("Vazio")]
        Vazio = 0,
        [Description("Professor Administrador")]
        ProfessorAdmin = 1,
        [Description("Professor")]
        Professor = 2,
        [Description("Aluno")]
        Aluno = 3
    }
}
