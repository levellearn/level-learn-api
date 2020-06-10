﻿using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Institucional;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Instituicao : Entity
    {
        #region Ctors

        protected Instituicao()
        {
            Cursos = new List<Curso>();
            Pessoas = new List<PessoaInstituicao>();
        }

        public Instituicao(string nome, string descricao)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            //Sigla = sigla.RemoveExtraSpaces().ToUpper();
            Cursos = new List<Curso>();
            Pessoas = new List<PessoaInstituicao>();

            AtribuirNomePesquisa();
        }

        #endregion Ctors

        #region Props

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public string Descricao { get; private set; }
        public string CNPJ { get; private set; }
        public string Municipio { get; private set; }
        public string UF { get; private set; }
        public string OrganizacaoAcademica { get; private set; }
        public string Rede { get; private set; }
        public string CategoriaAdministrativa { get; private set; }
        public string NivelEnsino { get; private set; }
        public string CEP { get; private set; }
        public string Endereco { get; private set; }
        public string Numero { get; private set; }

        public virtual ICollection<Curso> Cursos { get; private set; }
        public virtual ICollection<PessoaInstituicao> Pessoas { get; private set; }

        #endregion Props

        #region Methods

        public void Atualizar(string nome, string descricao)
        {
            Nome = nome.RemoveExtraSpaces().ToUpper();
            Descricao = descricao?.Trim();

            AtribuirNomePesquisa();
        }

        public void AtribuirCurso(Curso curso)
        {
            if (!curso.EstaValido()) return;

            Cursos.Add(curso);
        }

        public void AtribuirCursos(ICollection<Curso> cursos)
        {
            foreach (Curso curso in cursos)
            {
                if (curso.EstaValido())
                    Cursos.Add(curso);
            }
        }

        public void AtribuirPessoa(PessoaInstituicao pessoa)
        {
            Pessoas.Add(pessoa);
        }

        public void AtribuirPessoas(ICollection<PessoaInstituicao> pessoas)
        {
            foreach (PessoaInstituicao pessoa in pessoas)
            {
                Pessoas.Add(pessoa);
            }
        }

        public override bool EstaValido()
        {
            var validator = new InstituicaoValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return this.ResultadoValidacao.IsValid;
        }

        /// <summary>
        /// Ativação em cascata
        /// </summary>
        public override void Ativar()
        {
            base.Ativar();

            Cursos.ToList()
                .ForEach(c => c.Ativar());
        }

        /// <summary>
        /// Desativação em cascata
        /// </summary>
        public override void Desativar()
        {
            base.Desativar();

            Cursos.ToList()
                .ForEach(c => c.Desativar());
        }

        public override void AtribuirNomePesquisa()
        {
            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion Methods

    }

}
