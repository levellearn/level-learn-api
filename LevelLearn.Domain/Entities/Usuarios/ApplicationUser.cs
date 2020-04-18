using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities.Usuarios
{
    public class ApplicationUser : IdentityUser
    {
        private const string IMAGEM_URL_PADRAO = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/default-user.png?alt=media&token=5718efef-96f2-4933-802e-0aec94063608";

        protected ApplicationUser() { }

        public ApplicationUser(string nickName, string email, bool emailConfirmed, string senha, string confirmacaoSenha,
            string phoneNumber, bool phoneNumberConfirmed, Guid pessoaId)
        {
            Email = email;
            NormalizedEmail = email?.Trim()?.ToLower();
            EmailConfirmed = emailConfirmed;

            UserName = Email;
            NormalizedUserName = NormalizedEmail;

            ImagemUrl = IMAGEM_URL_PADRAO;

            NickName = nickName;
            Senha = senha ?? string.Empty;
            ConfirmacaoSenha = confirmacaoSenha ?? string.Empty;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            PessoaId = pessoaId;

            ResultadoValidacao = new ValidationResult();
        }

        public string NickName { get; set; }
        public string ImagemUrl { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public string Senha { get; } // Não Mapeado
        public string ConfirmacaoSenha { get; } // Não Mapeado
        public ValidationResult ResultadoValidacao { get; set; } // Não Mapeado

        // Methods 

        public bool EstaValido()
        {
            return this.ResultadoValidacao.IsValid;
        }

        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ResultadoValidacao.GetErrorsResult();
        }

        public void AlterarFotoPerfil(string imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl)) return;

            ImagemUrl = imagemUrl;
        }

    }
}
