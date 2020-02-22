using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Institucional;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Instituicao : EntityBase
    {
        #region Ctors

        protected Instituicao() { }

        public Instituicao(string nome, string descricao)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            Cursos = new List<Curso>();
            Pessoas = new List<PessoaInstituicao>();

            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion

        #region Props

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public ICollection<Curso> Cursos { get; private set; }
        public ICollection<PessoaInstituicao> Pessoas { get; private set; }

        #endregion

        #region Methods

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
            this.ValidationResult = validator.Validate(this);

            return this.ValidationResult.IsValid;
        } 

        #endregion

    }

}
