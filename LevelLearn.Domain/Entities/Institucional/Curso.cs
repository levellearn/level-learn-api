using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Institucional;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Curso : EntityBase
    {
        #region Ctors

        protected Curso() {
            Pessoas = new List<PessoaCurso>();
        }

        public Curso(string nome, string sigla, string descricao, Guid instituicaoId)
        {
            Nome = nome.RemoveExtraSpaces();
            Sigla = sigla.RemoveExtraSpaces().ToUpper();
            Descricao = descricao?.Trim();
            InstituicaoId = instituicaoId;
            Pessoas = new List<PessoaCurso>();

            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion Ctors

        #region Props

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public string Descricao { get; private set; }

        public Guid InstituicaoId { get; private set; }
        public virtual Instituicao Instituicao { get; private set; }
        public virtual ICollection<PessoaCurso> Pessoas { get; private set; }

        #endregion Props

        #region Methods

        public void AtribuirPessoa(PessoaCurso pessoa)
        {
            Pessoas.Add(pessoa);
        }

        public void AtribuirPessoas(ICollection<PessoaCurso> pessoas)
        {
            foreach (PessoaCurso pessoa in pessoas)
            {
                Pessoas.Add(pessoa);
            }
        }

        public override bool EstaValido()
        {
            var validator = new CursoValidator();
            this.ValidationResult = validator.Validate(this);
            
            return this.ValidationResult.IsValid;
        }

        #endregion Methods

    }
}
