using System;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class InstituicaoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
