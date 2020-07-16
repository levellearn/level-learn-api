using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Institucional.Turma;
using LevelLearn.ViewModel.Pessoas;
using System;
using System.Collections.Generic;

namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CursoDetalheVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        public Guid InstituicaoId { get; set; }
        public InstituicaoVM Instituicao { get; set; }

        public ICollection<PessoaCursoVM> Pessoas { get; set; }
        public ICollection<TurmaVM> Turmas { get; set; }
    }
}
