using LevelLearn.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Curso : EntityBase
    {
        // Ctors
        protected Curso() { }

        public Curso(string nome, string descricao)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            //Pessoas = new List<PessoaCurso>();
            Ativo = true;
            NomePesquisa = Nome.GenerateSlug();
        }

        public Curso(string nome, string descricao, Instituicao instituicao)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            //Pessoas = new List<PessoaCurso>();
            InstituicaoId = instituicao.Id;
            Instituicao = instituicao;
            Ativo = true;
            NomePesquisa = Nome.GenerateSlug();
        }

        // Props
        public string Nome { get; }
        public string Descricao { get; }
        public Guid InstituicaoId { get; }
        public virtual Instituicao Instituicao { get; private set; }

        //public ICollection<PessoaCurso> Pessoas { get; set; } = new List<PessoaCurso>()

        // Methods
        public void AtribuirInstituicao(Instituicao instituicao)
        {
            if (!instituicao.EstaValido()) return;

            this.Instituicao = instituicao;
        }      
        
        public override bool EstaValido()
        {
            throw new NotImplementedException();
            return this.ValidationResult.IsValid;
        }

    }
}
