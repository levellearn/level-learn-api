using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Identity;
using System;

namespace LevelLearn.Service.Services.Usuarios
{
    public class IdentityInitializer
    {
        private readonly LevelLearnContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            LevelLearnContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            //if (!_context.Database.EnsureCreated())
            //{
            //return;

            var perfilAdmin = PerfisInstituicao.Admin.ToString();
            if (!_roleManager.RoleExistsAsync(perfilAdmin).Result)
            {
                var identityRole = new IdentityRole() { Name = perfilAdmin, NormalizedName = perfilAdmin.ToLower() };
                var resultado = _roleManager.CreateAsync(identityRole).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception($"Erro durante a criação da role {perfilAdmin}.");
                }
            }

            var perfilProfessor = PerfisInstituicao.Professor.ToString();
            if (!_roleManager.RoleExistsAsync(perfilProfessor).Result)
            {
                var identityRole = new IdentityRole() { Name = perfilProfessor, NormalizedName = perfilProfessor.ToLower() };
                var resultado = _roleManager.CreateAsync(identityRole).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception($"Erro durante a criação da role {perfilProfessor}.");
                }
            }

            var perfilAluno = PerfisInstituicao.Aluno.ToString();
            if (!_roleManager.RoleExistsAsync(perfilAluno).Result)
            {
                var identityRole = new IdentityRole() { Name = perfilAluno, NormalizedName = perfilAluno.ToLower() };
                var resultado = _roleManager.CreateAsync(identityRole).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception($"Erro durante a criação da role {perfilAluno}.");
                }
            }

            //CreateUser(
            //    new ApplicationUser()
            //    {
            //        UserName = "admin_apiprodutos",
            //        Email = "admin-apiprodutos@teste.com.br",
            //        EmailConfirmed = true
            //    }, "AdminAPIProdutos01!", Roles.ROLE_API_PRODUTOS);

            //CreateUser(
            //    new ApplicationUser()
            //    {
            //        UserName = "usrinvalido_apiprodutos",
            //        Email = "usrinvalido-apiprodutos@teste.com.br",
            //        EmailConfirmed = true
            //    }, "UsrInvAPIProdutos01!");
        }
        //private void CreateUser(ApplicationUser user, string password, string initialRole = null)
        //{
        //    if (_userManager.FindByNameAsync(user.UserName).Result == null)
        //    {
        //        var resultado = _userManager
        //            .CreateAsync(user, password).Result;

        //        if (resultado.Succeeded &&
        //            !String.IsNullOrWhiteSpace(initialRole))
        //        {
        //            _userManager.AddToRoleAsync(user, initialRole).Wait();
        //        }
        //    }
        //}


    }
}
