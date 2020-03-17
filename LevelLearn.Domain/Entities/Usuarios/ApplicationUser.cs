using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using System;

namespace LevelLearn.Domain.Entities.Usuarios
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName, string email, 
            bool emailConfirmed, string phoneNumber, bool phoneNumberConfirmed, Pessoa pessoa)
        {
            UserName = userName;
            NormalizedUserName = userName.GenerateSlug();
            Email = email;
            NormalizedEmail = email.GenerateSlug();
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            Pessoa = pessoa;
            PessoaId = pessoa.Id;
        }

        public Guid PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
