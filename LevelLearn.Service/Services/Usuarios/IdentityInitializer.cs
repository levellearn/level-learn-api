using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Service.Services.Usuarios
{
    /// <summary>
    /// Criação de estruturas, usuários e permissões Identity
    /// </summary>
    public class IdentityInitializer
    {
        private readonly LevelLearnContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(LevelLearnContext context, UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            CreateRole(ApplicationRoles.ADMIN);
            CreateRole(ApplicationRoles.PROFESSOR);
            CreateRole(ApplicationRoles.ALUNO);

            var email = "felipe.ayres93@gmail.com";
            var celular = "(12)98845-7832";
            var pessoa = new Admin("Felipe Ayres", new Email(email), new CPF("226.547.010-42"),
               new Celular(celular), Generos.Masculino, DateTime.Parse("1993-10-26"));

            var user = new Usuario(pessoa.Nome, "felipe.ayres", email, celular, pessoa.Id);
            user.AtribuirSenha("Gamificando@123", "Gamificando@123");
            user.ConfirmarEmail();
            user.ConfirmarCelular();

            var roles = new List<string>() { ApplicationRoles.ADMIN, ApplicationRoles.PROFESSOR, ApplicationRoles.ALUNO };
            CreateUser(user, pessoa, roles);
        }

        private void CreateRole(string role)
        {
            if (_roleManager.RoleExistsAsync(role).Result)
                return;

            var identityRole = new IdentityRole()
            {
                Name = role,
                NormalizedName = role.ToUpper()
            };

            var result = _roleManager.CreateAsync(identityRole).Result;

            if (!result.Succeeded)
                throw new Exception($"Erro durante a criação da role {role}.");
        }

        private void CreateUser(Usuario user, Pessoa pessoa, ICollection<string> roles)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return;

            _context.Pessoas.Add(pessoa);

            if (_context.SaveChanges() == 0) return;

            var result = _userManager.CreateAsync(user, user.Senha).Result;

            if (result.Succeeded && roles.Any())
                _userManager.AddToRolesAsync(user, roles).Wait();
        }


    }
}
