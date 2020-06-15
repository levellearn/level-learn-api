using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Service.Services.Comum
{
    /// <summary>
    /// Criação de estruturas banco, usuários e permissões Identity
    /// </summary>
    public class DatabaseSeed
    {
        private readonly LevelLearnContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public DatabaseSeed(LevelLearnContext context, UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
        }

        public void Initialize()
        {
            CriarRole(ApplicationRoles.ADMIN);
            CriarRole(ApplicationRoles.PROFESSOR);
            CriarRole(ApplicationRoles.ALUNO);

            var email = "felipe.ayres93@gmail.com";
            var celular = "(12)98845-7832";
            var pessoa = new Admin("Felipe Ayres", new Email(email), new CPF("226.547.010-42"),
               new Celular(celular), GeneroPessoa.Masculino, DateTime.Parse("1993-10-26"));

            var usuario = new Usuario(pessoa.Nome, "felipe.ayres", email, celular, pessoa.Id);
            usuario.AtribuirSenha("Gamificando@123", "Gamificando@123");
            usuario.ConfirmarEmail();
            usuario.ConfirmarCelular();

            var roles = new List<string>() { ApplicationRoles.ADMIN, ApplicationRoles.PROFESSOR, ApplicationRoles.ALUNO };

            CriarUsuario(usuario, pessoa, roles);           

            CriarInstituicao(pessoa);
        }

        private void CriarRole(string role)
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

        private void CriarUsuario(Usuario user, Pessoa pessoa, ICollection<string> roles)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return;

            _context.Pessoas.Add(pessoa);

            if (_context.SaveChanges() == 0) return;

            var result = _userManager.CreateAsync(user, user.Senha).Result;

            if (result.Succeeded && roles.Any())
                _userManager.AddToRolesAsync(user, roles).Wait();
        }

        private void CriarInstituicao(Pessoa pessoa)
        {
            if (!_env.IsDevelopment()) return;

            var descricao = "Criada em 1994, a FATEC Guaratinguetá tem como objetivo promover a educação profissional pública oferecendo cursos de graduação em Tecnologia, formando Tecnólogos dentro de referenciais de excelência, visando ao atendimento das demandas sociais e do mundo do trabalho.";

            var instituicao = new Instituicao("FACULDADE DE TECNOLOGIA DE GUARATINGUETÁ", "FATEC GT", descricao, "62823257000109", OrganizacaoAcademica.Faculdade, Rede.Publica, CategoriaAdministrativa.Estadual, NivelEnsino.Superior, "12517010", "GUARATINGUETÁ", "SP");

            if (_context.Instituicoes.Any(i => i.NomePesquisa == instituicao.NomePesquisa))
                return;

            var pessoaInstituicao = new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, pessoa.Id, instituicao.Id);
            instituicao.AtribuirPessoa(pessoaInstituicao);

            _context.Add(instituicao);
            _context.SaveChanges();

            CriarCurso(instituicao, pessoa);
        }

        private void CriarCurso(Instituicao instituicao, Pessoa pessoa)
        {
            if (!_env.IsDevelopment()) return;

            var descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";

            var curso = new Curso("Análise e Desenvolvimento de Sistemas", "ADS", descricao, instituicao.Id);

            if (_context.Cursos.Any(i => i.NomePesquisa == curso.NomePesquisa))
                return;

            var pessoaCurso = new PessoaCurso(TipoPessoa.Professor, pessoa.Id, curso.Id);
            curso.AtribuirPessoa(pessoaCurso);

            _context.Add(curso);
            _context.SaveChanges();
        }

    }
}
