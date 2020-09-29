using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Usuarios;
using LevelLearn.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Usuarios
{
    public class Usuario : IdentityUser
    {
        #region Ctors

        private const string IMAGEM_URL_PADRAO = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/default-user.png?alt=media&token=5718efef-96f2-4933-802e-0aec94063608";

        protected Usuario() { }

        public Usuario(string nome, string nickName, string email, string celular, string senha, string confirmacaoSenha)
        {
            Nome = nome.RemoveExtraSpaces();
            Email = email;
            NormalizedEmail = email?.Trim()?.ToLower();
            UserName = Email;
            NormalizedUserName = NormalizedEmail;
            ImagemUrl = IMAGEM_URL_PADRAO;
            NickName = nickName?.Trim() ?? string.Empty;
            PhoneNumber = celular;
            this.Senha = senha ?? string.Empty;
            this.ConfirmacaoSenha = confirmacaoSenha ?? string.Empty;

            ResultadoValidacao = new ValidationResult();
        }

        #endregion

        #region Props

        public string Nome { get; set; } // Redundância pessoa
        public string NickName { get; set; } // Rota alterar NickName
        public string ImagemUrl { get; set; }
        public string ImagemNome { get; set; }
        public Guid PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        // Não Mapeado
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public ValidationResult ResultadoValidacao { get; set; }

        #endregion

        #region Methods
        public bool EstaValido()
        {
            var validator = new UsuarioValidator();
            this.ResultadoValidacao = validator.Validate(this);

            // VOs
            ValidarEmail();
            ValidarSenha();

            return this.ResultadoValidacao.IsValid;
        }

        private void ValidarEmail()
        {
            var emailVO = new Email(this.Email);

            if (emailVO.EstaValido()) return;
            this.ResultadoValidacao.AddErrors(emailVO.ResultadoValidacao);
        }

        private void ValidarSenha()
        {
            var senhaVO = new Senha(this.Senha);

            if (senhaVO.EstaValido()) return;
            this.ResultadoValidacao.AddErrors(senhaVO.ResultadoValidacao);
        }

        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ResultadoValidacao.GetErrorsResult();
        }

        public void AtribuirPessoaId(Guid pessoaId)
        {
            PessoaId = pessoaId;
        }

        public void ConfirmarEmail()
        {
            this.EmailConfirmed = true;
        }

        public void ConfirmarCelular()
        {
            this.PhoneNumberConfirmed = true;
        }

        /// <summary>
        /// Gera um nome para a imagem com base no ID usuário e um timestamp 
        /// </summary>
        /// <returns></returns>
        public string GerarNomeFotoPerfil()
        {
            var nomeImagem = this.Id.ToString() + "_" + DateTime.Now.Ticks.ToString();

            return nomeImagem;
        }

        /// <summary>
        /// Altera a ImagemUrl da foto de perfil, retornando o nome da imagem anterior
        /// </summary>
        /// <param name="imagemUrl">Url imagem de perfil do storage</param>
        /// <param name="imagemNome">Nome da imagem nova</param>
        /// <returns>Retorna o nome da imagem anterior</returns>
        public string AlterarFotoPerfil(string imagemUrl, string imagemNome)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl)) return string.Empty;
            if (string.IsNullOrWhiteSpace(imagemNome)) return string.Empty;

            string nomeImagemAnterior = this.ImagemNome;

            this.ImagemUrl = imagemUrl;
            this.ImagemNome = imagemNome;

            return nomeImagemAnterior;
        }

        #endregion

    }
}
