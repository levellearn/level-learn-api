using System;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CadastrarCursoVM
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public Guid InstituicaoId { get; set; }
    }
}
