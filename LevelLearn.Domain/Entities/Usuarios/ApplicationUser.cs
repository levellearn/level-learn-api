﻿using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Pessoas;
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
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            PessoaId = pessoaId;

            ValidationResult = new ValidationResult();
        }

        public string NickName { get; set; }
        public string Senha { get; }
        public string ConfirmacaoSenha { get; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public ValidationResult ValidationResult { get; private set; }

        public bool EstaValido()
        {
            var validator = new UsuarioValidator();
            this.ValidationResult = validator.Validate(this);

            // Validações adicionais
            if (string.IsNullOrWhiteSpace(ConfirmacaoSenha))
                this.ValidationResult.Errors.Add(new ValidationFailure(nameof(ConfirmacaoSenha), "Confirmação de senha precisa estar preenchida"));

            if (Senha != ConfirmacaoSenha)
                this.ValidationResult.Errors.Add(new ValidationFailure(nameof(Senha), "Senha e confirmação de senha não conferem"));

            return this.ValidationResult.IsValid;
        }

        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ValidationResult.GetErrorsResult();
        }

    }
}
