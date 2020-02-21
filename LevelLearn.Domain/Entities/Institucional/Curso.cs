using LevelLearn.Domain.Entities.Pessoas;
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
        
        public Curso(string nome, string descricao, Guid instituicaoId)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            InstituicaoId = instituicaoId;
            Pessoas = new List<PessoaCurso>();

            NomePesquisa = Nome.GenerateSlug();
        }

        // Props
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid InstituicaoId { get; private set; }

        public virtual Instituicao Instituicao { get; private set; }
        public ICollection<PessoaCurso> Pessoas { get; private set; }

        // Methods        
        
        public override bool EstaValido()
        {
            throw new NotImplementedException();
            return this.ValidationResult.IsValid;
        }

    }
}
