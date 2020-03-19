using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Pessoas;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevelLearn.Domain.Entities.Usuarios
{
    public class ApplicationUser : IdentityUser
    {
        protected ApplicationUser() { }

        public ApplicationUser(string userName, string email, bool emailConfirmed, string senha, string confirmacaoSenha,
            string phoneNumber, bool phoneNumberConfirmed, Pessoa pessoa)
        {
            UserName = userName;
            NormalizedUserName = userName?.Trim()?.ToLower();
            Email = email;
            NormalizedEmail = email?.Trim()?.ToLower();
            EmailConfirmed = emailConfirmed;
            Senha = senha?.Trim() ?? string.Empty;
            ConfirmacaoSenha = confirmacaoSenha?.Trim() ?? string.Empty;
            PhoneNumber = phoneNumber.GetNumbers();
            PhoneNumber = PhoneNumber.StartsWith("55") ? PhoneNumber : PhoneNumber.Insert(0, "55");
            PhoneNumberConfirmed = phoneNumberConfirmed;
            Pessoa = pessoa;
            PessoaId = pessoa.Id;

            ValidationResult = new ValidationResult();
        }

        public string Senha { get; }
        public string ConfirmacaoSenha { get; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public ValidationResult ValidationResult { get; private set; }

        public bool EstaValido()
        {
            var validator = new UsuarioValidator();
            this.ValidationResult = validator.Validate(this);

            return this.ValidationResult.IsValid;
        }

        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ValidationResult.GetErrorsResult();
        }

    }
}
