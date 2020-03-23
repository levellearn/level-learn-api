using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Identity;
using System;

namespace LevelLearn.Service.Services.Usuarios
{
    /// <summary>
    /// Criação de estruturas, usuários e permissões Identity
    /// </summary>
    public class IdentityInitializer
    {
        private readonly LevelLearnContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(LevelLearnContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            CreateRole(ApplicationRoles.ADMIN);
            CreateRole(ApplicationRoles.PROFESSOR);
            CreateRole(ApplicationRoles.ALUNO);

            var email = "felipe.ayres93@gmail.com";
            var celular = "(12)98845-7832";
            var pessoa = new Admin("Felipe Ayres", "felipe.ayres", new Email(email), new CPF("226.547.010-42"),
               new Celular(celular), Generos.Masculino, imagemUrl: null, DateTime.Parse("1993-10-26"));

            var user = new ApplicationUser(pessoa.NickName, email, true, "Gamificando@123", "Gamificando@123",
                celular, true, pessoa.Id);

            CreateUser(user, pessoa, ApplicationRoles.ADMIN);
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

        private void CreateUser(ApplicationUser user, Pessoa pessoa, string initialRole)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return;

            _context.Pessoas.Add(pessoa);

            if (_context.SaveChanges() == 0) return;

            var result = _userManager.CreateAsync(user, user.Senha).Result;

            if (result.Succeeded && !string.IsNullOrWhiteSpace(initialRole))
                _userManager.AddToRoleAsync(user, initialRole).Wait();
        }


    }
}
