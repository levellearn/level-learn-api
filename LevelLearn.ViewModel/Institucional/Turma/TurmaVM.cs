using System;

namespace LevelLearn.ViewModel.Institucional.Turma
{
    public class TurmaVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeDisciplina { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid CursoId { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
