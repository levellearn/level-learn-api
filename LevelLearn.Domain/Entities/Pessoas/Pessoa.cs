using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Pessoas;
using LevelLearn.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public abstract class Pessoa : EntityBase
    {
        private const string IMAGEM_URL_PADRAO = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";

        #region Ctors
        protected Pessoa()
        {
            Instituicoes = new List<PessoaInstituicao>();
        }

        public Pessoa(string nome, string nickName, Email email, CPF cpf, Celular celular, Generos genero,
            string imagemUrl, DateTime? dataNascimento)
        {
            Nome = nome.RemoveExtraSpaces();
            NickName = nickName?.Trim() ?? string.Empty; // TODO: Username único?
            Email = email;
            Cpf = cpf;
            Celular = celular;
            Genero = genero;
            ImagemUrl = string.IsNullOrWhiteSpace(imagemUrl) ? IMAGEM_URL_PADRAO : imagemUrl;
            DataNascimento = dataNascimento;
            Instituicoes = new List<PessoaInstituicao>();

            NomePesquisa = Nome.GenerateSlug();
        }

        #endregion Ctors

        #region Props
        public string Nome { get; protected set; }
        public string NickName { get; protected set; }
        public Email Email { get; protected set; }
        public CPF Cpf { get; protected set; }
        public Celular Celular { get; protected set; }
        public Generos Genero { get; protected set; }
        public TiposPessoa TipoPessoa { get; protected set; }
        public string ImagemUrl { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }

        public virtual ICollection<PessoaInstituicao> Instituicoes { get; protected set; }

        #endregion Props


        #region Methods

        public override bool EstaValido()
        {
            var validator = new PessoaValidator();

            this.ValidationResult = validator.Validate(this);

            // VOs
            ValidarCPF();
            ValidarEmail();
            ValidarCelular();

            return this.ValidationResult.IsValid;
        }

        protected void ValidarCPF()
        {
            if (Cpf.EstaValido()) return;
            this.ValidationResult.AddErrors(Cpf.ValidationResult);
        }

        protected void ValidarEmail()
        {
            if (Email.EstaValido()) return;
            this.ValidationResult.AddErrors(Email.ValidationResult);
        }

        protected void ValidarCelular()
        {
            if (Celular.EstaValido()) return;
            this.ValidationResult.AddErrors(Celular.ValidationResult);
        }

        public void AtribuirPessoa(PessoaInstituicao instituicao)
        {
            //if (!instituicao.EstaValido()) return;

            Instituicoes.Add(instituicao);
        }

        public void AtribuirPessoas(ICollection<PessoaInstituicao> instituicoes)
        {
            foreach (PessoaInstituicao instituicao in instituicoes)
            {
                //if (instituicao.EstaValido())
                Instituicoes.Add(instituicao);
            }
        }

        public override string ToString()
        {
            var dataNascimento = DataNascimento.GetValueOrDefault(new DateTime());

            return $"ID: {Id}" +
                $" CPF: {Cpf.ToString()}" +
                $" Nome: {Nome}" +
                $" NickName: {NickName}" +
                $" E-mail: {Email.ToString()}" +
                $" Celular: {Celular.ToString()} " +
                $" Gênero: {Genero.ToString()} " +
                $" Tipo Pessoa: {TipoPessoa.ToString()} " +
                $" Imagem: {ImagemUrl} " +
                $" Data Nascimento: {dataNascimento.ToString("dd/MM/yyyy")}" +
                $" Data Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}" +
                $" Ativo: { (Ativo ? "Sim" : "Não") }";
        }

        #endregion Methods


    }
}
