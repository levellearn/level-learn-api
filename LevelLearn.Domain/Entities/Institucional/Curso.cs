using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Institucional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Curso : Entity
    {
        #region Ctors

        protected Curso()
        {
            Pessoas = new List<PessoaCurso>();
            Turmas = new List<Turma>();
        }

        public Curso(string nome, string sigla, string descricao, Guid instituicaoId)
        {
            Nome = nome.RemoveExtraSpaces().ToUpper();
            Sigla = sigla.RemoveExtraSpaces().ToUpper();
            Descricao = descricao?.Trim();
            InstituicaoId = instituicaoId;

            Pessoas = new List<PessoaCurso>();
            Turmas = new List<Turma>();

            AtribuirNomePesquisa();
        }

        #endregion Ctors

        #region Props

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public string Descricao { get; private set; }

        public Guid InstituicaoId { get; private set; }
        public virtual Instituicao Instituicao { get; private set; }

        public virtual ICollection<PessoaCurso> Pessoas { get; private set; }
        public virtual ICollection<Turma> Turmas { get; private set; }


        #endregion Props

        #region Methods

        public void Atualizar(string nome, string sigla, string descricao)
        {
            Nome = nome.RemoveExtraSpaces().ToUpper();
            Sigla = sigla.RemoveExtraSpaces().ToUpper();
            Descricao = descricao?.Trim();

            AtribuirNomePesquisa();
        }

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

        public void AtribuirTurma(Turma turma)
        {
            Turmas.Add(turma);
        }

        public void AtribuirTurmas(ICollection<Turma> turmas)
        {
            foreach (Turma turma in turmas)
            {
                Turmas.Add(turma);
            }
        }

        public override bool EstaValido()
        {
            var validator = new CursoValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return this.ResultadoValidacao.IsValid;
        }

        /// <summary>
        /// Ativação em cascata
        /// </summary>
        public override void Ativar()
        {
            base.Ativar();

            Turmas.ToList()
                .ForEach(c => c.Ativar());
        }

        /// <summary>
        /// Desativação em cascata
        /// </summary>
        public override void Desativar()
        {
            base.Desativar();

            Turmas.ToList()
                .ForEach(c => c.Desativar());
        }

        public override void AtribuirNomePesquisa()
        {
            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion Methods

    }
}
