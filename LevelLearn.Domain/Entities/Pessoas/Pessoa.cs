using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
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

        public Pessoa(string nome, Email email, CPF cpf, Celular celular, Generos genero, DateTime? dataNascimento)
        {
            Nome = nome.RemoveExtraSpaces();
            Email = email;
            Cpf = cpf;
            Celular = celular;
            Genero = genero;
            DataNascimento = dataNascimento;

            Instituicoes = new List<PessoaInstituicao>();
            Cursos = new List<PessoaCurso>();
            Turmas = new List<Turma>();
            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion Ctors

        #region Props
        public string Nome { get; protected set; }
        public Email Email { get; protected set; }
        public CPF Cpf { get; protected set; }
        public Celular Celular { get; protected set; }
        public Generos Genero { get; protected set; }
        public TiposPessoa TipoPessoa { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }

        public virtual ICollection<PessoaInstituicao> Instituicoes { get; protected set; }
        public virtual ICollection<PessoaCurso> Cursos { get; protected set; }
        public virtual ICollection<Turma> Turmas { get; protected set; }

        #endregion Props

        #region Methods

        public override bool EstaValido()
        {
            return this.ResultadoValidacao.IsValid;
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

        public override string ToString()
        {
            var dataNascimento = DataNascimento.GetValueOrDefault(new DateTime());

            return $"ID: {Id}" +
                $" CPF: {Cpf.ToString()}" +
                $" Nome: {Nome}" +
                $" E-mail: {Email.ToString()}" +
                $" Celular: {Celular.ToString()} " +
                $" Gênero: {Genero.ToString()} " +
                $" Tipo Pessoa: {TipoPessoa.ToString()} " +
                $" Data Nascimento: {dataNascimento.ToString("dd/MM/yyyy")}" +
                $" Data Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}" +
                $" Ativo: { (Ativo ? "Sim" : "Não") }";
        }

        #endregion Methods


    }
}
