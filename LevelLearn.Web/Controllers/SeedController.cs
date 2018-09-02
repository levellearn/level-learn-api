using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Web.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Web.Controllers
{
    public class SeedController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeedController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Roles()
        {
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "ALUNO",
                    NormalizedName =  "Aluno"
                },
                new IdentityRole
                {
                    Name = "PROF",
                    NormalizedName = "Professor"
                },
                new IdentityRole
                {
                    Name = "ADMIN",
                    NormalizedName =  "Administrador"
                }
            };

            foreach (var item in roles)
            {
                IdentityResult result = Task.Run(() => _roleManager.CreateAsync(item)).Result;
            }


        }

        public async Task<IActionResult> Usuarios()
        {
            Roles();
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                        UserName = "leo_barbetta@outlook.com",
                        Email = "leo_barbetta@outlook.com",
                        Pessoa = new Pessoa()
                        {
                            Nome = "Leonardo Barbetta de Oliveira",
                            UserName = "leobarbetta",
                            Email = "leo_barbetta@outlook.com",
                            Imagem = "/teste",
                            Sexo = SexoEnum.Masculino,
                            TipoPessoa =  TipoPessoaEnum.Professor
                        },
                },
                new ApplicationUser
                {
                        UserName = "gggfaria@outlook.com",
                        Email = "gggfaria@outlook.com",
                        Pessoa = new Pessoa()
                        {
                            Nome = "Gabriel Galvão Guimarães Faria",
                            UserName = "gggfaria",
                            Email = "gggfaria@outlook.com",
                            Imagem = "/teste",
                            Sexo = SexoEnum.Masculino,
                            TipoPessoa =  TipoPessoaEnum.Aluno
                        },
                },
                new ApplicationUser
                {
                        UserName = "le.guarino@gmail.com",
                        Email = "le.guarino@gmail.com",
                        Pessoa = new Pessoa()
                        {
                            Nome = "Leandro Guarino",
                            UserName = "le.guarino",
                            Email = "le.guarino@gmail.com",
                            Imagem = "/teste",
                            Sexo = SexoEnum.Masculino,
                            TipoPessoa =  TipoPessoaEnum.Professor
                        },
                },
            };

            int sucess = 0;
            int errs = 0;

            foreach (var item in users)
            {
                IdentityResult result;
                if (item.Pessoa.TipoPessoa == TipoPessoaEnum.Aluno)
                {
                    result = await _userManager.CreateAsync(item, "root123");
                    await _userManager.AddToRoleAsync(item, "ALUNO");
                }
                else
                {
                    if (item.UserName == "leo_barbetta@outlook.com")
                    {
                        result = await _userManager.CreateAsync(item, "root123");
                        await _userManager.AddToRoleAsync(item, "ADMIN");
                    }
                    else
                    {
                        result = await _userManager.CreateAsync(item, "root123");
                        await _userManager.AddToRoleAsync(item, "PROF");
                    }
                }

                if (result.Succeeded)
                    sucess++;
                else
                    errs++;
            }

            return Json(new { Sucesso = $"Sucesso {sucess} - Error {errs}" });
        }
    }
}