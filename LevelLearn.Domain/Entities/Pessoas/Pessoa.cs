using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Pessoas;
using LevelLearn.Domain.ValueObjects;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public abstract class Pessoa : EntityBase
    {
        // Ctors
        protected Pessoa() { }

        public Pessoa(string nome, string userName, Email email, CPF cpf, Celular celular, Generos genero,
            string imagemUrl, DateTime? dataNascimento)
        {
            Nome = nome.RemoveExtraSpaces().ToUpper();
            UserName = userName.RemoveExtraSpaces(); // TODO: username único?
            Email = email;
            Cpf = cpf;
            Celular = celular;
            Genero = genero;
            ImagemUrl = imagemUrl; // TODO: Obrigatório?
            DataNascimento = dataNascimento;
            DataCadastro = DateTime.Now;
            Ativo = true;
            NomePesquisa = Nome.GenerateSlug();
        }

        // Props
        public string Nome { get; protected set; }
        public string UserName { get; protected set; }
        public Email Email { get; protected set; }
        public CPF Cpf { get; protected set; }
        public Celular Celular { get; protected set; }
        public Generos Genero { get; protected set; }
        public TiposPessoa TipoPessoa { get; protected set; }
        public string ImagemUrl { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }
        public DateTime DataCadastro { get; protected set; }        

        //public ICollection<PessoaInstituicao> Instituicoes { get; set; } = new List<PessoaInstituicao>();

        // Methods
        
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

        public override string ToString()
        {
            var dataNascimento = DataNascimento.GetValueOrDefault(new DateTime());

            return $"ID: {Id}" +
                $" CPF: {Cpf.ToString()}" +
                $" Nome: {Nome}" +
                $" UserName: {UserName}" +
                $" E-mail: {Email.ToString()}" +
                $" Celular: {Celular.ToString()} " +
                $" Gênero: {Genero.ToString()} " +
                $" Tipo Pessoa: {TipoPessoa.ToString()} " +
                $" Imagem: {ImagemUrl} " +
                $" Data Nascimento: {dataNascimento.ToString("dd/MM/yyyy")}" +
                $" Data Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}" +
                $" Ativo: { (Ativo ? "Sim" : "Não") }";
        }


    }
}
