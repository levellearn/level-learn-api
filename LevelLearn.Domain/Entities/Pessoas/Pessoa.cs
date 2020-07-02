using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public abstract class Pessoa : EntityBase
    {
        #region Ctors

        protected Pessoa()
        {
            Instituicoes = new List<PessoaInstituicao>();
            Cursos = new List<PessoaCurso>();
            Turmas = new List<Turma>();
        }

        public Pessoa(string nome, CPF cpf, Celular celular, GeneroPessoa genero, DateTime? dataNascimento)
        {
            Nome = nome.RemoveExtraSpaces();
            Cpf = cpf;
            Celular = celular;
            Genero = genero;
            DataNascimento = dataNascimento;

            Instituicoes = new List<PessoaInstituicao>();
            Cursos = new List<PessoaCurso>();
            Turmas = new List<Turma>();

            AtribuirNomePesquisa();
        }

        #endregion Ctors

        #region Props
        public string Nome { get; protected set; }
        public CPF Cpf { get; protected set; }
        public Celular Celular { get; protected set; }
        public GeneroPessoa Genero { get; protected set; }
        public TipoPessoa TipoPessoa { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }

        public virtual ICollection<PessoaInstituicao> Instituicoes { get; protected set; }
        public virtual ICollection<PessoaCurso> Cursos { get; protected set; }
        public virtual ICollection<Turma> Turmas { get; protected set; }

        #endregion Props

        #region Methods

        public override bool EstaValido()
        {
            var validator = new PessoaValidator();

            this.ResultadoValidacao = validator.Validate(this);

            // VOs
            ValidarCPF();
            ValidarCelular();

            return this.ResultadoValidacao.IsValid;
        }

        protected void ValidarCPF()
        {
            if (Cpf.EstaValido()) return;
            this.ResultadoValidacao.AddErrors(Cpf.ResultadoValidacao);
        }       

        protected void ValidarCelular()
        {
            if (Celular.EstaValido()) return;
            this.ResultadoValidacao.AddErrors(Celular.ResultadoValidacao);
        }

        public void AtribuirInstituicao(PessoaInstituicao instituicao)
        {
            Instituicoes.Add(instituicao);
        }

        public void AtribuirInstituicoes(ICollection<PessoaInstituicao> instituicoes)
        {
            foreach (PessoaInstituicao instituicao in instituicoes)
            {
                Instituicoes.Add(instituicao);
            }
        }

        public void AtribuirCurso(PessoaCurso curso)
        {
            Cursos.Add(curso);
        }

        public void AtribuirCursos(ICollection<PessoaCurso> cursos)
        {
            foreach (PessoaCurso curso in cursos)
            {
                Cursos.Add(curso);
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

        public override void AtribuirNomePesquisa()
        {
            NomePesquisa = Nome.GenerateSlug();
        }

        public override string ToString()
        {
            return $"ID: {Id}" +
                $" CPF: {Cpf}" +
                $" Nome: {Nome}" +
                $" Celular: {Celular} " +
                $" Gênero: {Genero} " +
                $" Tipo Pessoa: {TipoPessoa} " +
                $" Data Nascimento: {DataNascimento}" +
                $" Data Cadastro: {DataCadastro}" +
                $" Ativo: { (Ativo ? "Sim" : "Não") }";
        }

        #endregion Methods


    }
}
