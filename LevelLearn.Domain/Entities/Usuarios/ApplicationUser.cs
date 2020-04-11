﻿using FluentValidation.Results;
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
        protected ApplicationUser() { }

        public ApplicationUser(string nickName, string email, bool emailConfirmed, string senha, string confirmacaoSenha,
            string phoneNumber, bool phoneNumberConfirmed, Guid pessoaId)
        {
            Email = email;
            NormalizedEmail = email?.Trim()?.ToLower();
            EmailConfirmed = emailConfirmed;

            UserName = Email;
            NormalizedUserName = NormalizedEmail;

            NickName = nickName;
            Senha = senha ?? string.Empty;
            ConfirmacaoSenha = confirmacaoSenha ?? string.Empty;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            PessoaId = pessoaId;

            ResultadoValidacao = new ValidationResult();
        }

        public string NickName { get; set; }
        public string Senha { get; }
        public string ConfirmacaoSenha { get; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public ValidationResult ResultadoValidacao { get; set; }

        public bool EstaValido()
        {
            return this.ResultadoValidacao.IsValid;
        }

        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ResultadoValidacao.GetErrorsResult();
        }

    }
}
