using System;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CursoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid InstituicaoId { get; set; }
    }
}
