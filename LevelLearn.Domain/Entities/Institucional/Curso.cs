﻿using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Curso : EntityBase
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
        public virtual ICollection<Turma> Turmas { get; private set; }


        #endregion Props

        #region Methods

        public void Atualizar(string nome, string sigla, string descricao)
        {
            Nome = nome.RemoveExtraSpaces().ToUpper();
            Sigla = sigla.RemoveExtraSpaces().ToUpper();
            Descricao = descricao?.Trim();

            NomePesquisa = Nome.GenerateSlug();
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


        #endregion Methods

    }
}
