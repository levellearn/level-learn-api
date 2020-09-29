using System;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CadastrarTurmaVM
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeDisciplina { get; set; }
        public Guid CursoId { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
