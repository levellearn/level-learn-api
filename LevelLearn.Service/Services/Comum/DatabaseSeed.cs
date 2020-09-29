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
            CriarRole(UserRoles.ADMIN);
            CriarRole(UserRoles.PROFESSOR);
            CriarRole(UserRoles.ALUNO);

            var professor = CriarProfessor();
            var aluno = CriarAluno();

            var instituicao = CriarInstituicao();
            if (instituicao == null) return;
            instituicao.AtribuirPessoa(new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, professor.Id, instituicao.Id));
            instituicao.AtribuirPessoa(new PessoaInstituicao(PerfilInstituicao.Aluno, aluno.Id, instituicao.Id));

            var curso = CriarCurso(instituicao);
            if (curso == null) return;
            curso.AtribuirPessoa(new PessoaCurso(TipoPessoa.Professor, professor.Id, curso.Id));
            curso.AtribuirPessoa(new PessoaCurso(TipoPessoa.Aluno, aluno.Id, curso.Id));

            var turma = CriarTurma(curso, professor);
            if (turma == null) return;
            turma.AtribuirAluno(new AlunoTurma(aluno.Id, turma.Id));

            _context.SaveChanges();
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

        private Professor CriarProfessor()
        {
            if (!_env.IsDevelopment()) return null;

            var pessoa = new Professor("Felipe Ayres", new CPF("226.547.010-42"),
               new Celular("55(12)98845-7832"), GeneroPessoa.Masculino, DateTime.Parse("1993-10-26"));

            var usuario = new Usuario(pessoa.Nome, "felipe.ayres", "felipe.ayres93@gmail.com", "55(12)98845-7832", "Gamificando@123", "Gamificando@123");
            usuario.AtribuirPessoaId(pessoa.Id);
            usuario.ConfirmarEmail();
            usuario.ConfirmarCelular();

            var roles = new List<string>() { UserRoles.PROFESSOR };

            CriarUsuario(usuario, pessoa, roles);

            return pessoa;
        }

        private Aluno CriarAluno()
        {
            if (!_env.IsDevelopment()) return null;

            var email = "gabrielguima93@gmail.com";
            var celular = "55(12)98845-7832";

            var aluno = new Aluno("Gabriel Guimarães", new CPF("200.481.690-21"), new Celular(celular), "f1310435", GeneroPessoa.Masculino, DateTime.Parse("1993-10-23"));

            var usuario = new Usuario(aluno.Nome, "gabrielguima93", email, celular, "Gamificando@123", "Gamificando@123");
            usuario.AtribuirPessoaId(aluno.Id);
            usuario.ConfirmarEmail();
            usuario.ConfirmarCelular();

            var roles = new List<string>() { UserRoles.ALUNO };
            CriarUsuario(usuario, aluno, roles);

            return aluno;
        }

        private Usuario CriarUsuario(Usuario user, Pessoa pessoa, ICollection<string> roles)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return null;

            _context.Pessoas.Add(pessoa);

            if (_context.SaveChanges() == 0) return null;

            var result = _userManager.CreateAsync(user, user.Senha).Result;

            if (result.Succeeded && roles.Any())
                _userManager.AddToRolesAsync(user, roles).Wait();

            return user;
        }

        private Instituicao CriarInstituicao()
        {
            if (!_env.IsDevelopment()) return null;

            var descricao = "Criada em 1994, a FATEC Guaratinguetá tem como objetivo promover a educação profissional pública oferecendo cursos de graduação em Tecnologia, formando Tecnólogos dentro de referenciais de excelência, visando ao atendimento das demandas sociais e do mundo do trabalho.";

            var instituicao = new Instituicao("FACULDADE DE TECNOLOGIA DE GUARATINGUETÁ", "FATEC GT", descricao, "62823257000109", OrganizacaoAcademica.Faculdade, Rede.Publica, CategoriaAdministrativa.Estadual, NivelEnsino.Superior, "12517010", "GUARATINGUETÁ", "SP");

            if (_context.Instituicoes.Any(i => i.NomePesquisa == instituicao.NomePesquisa))
                return null;

            _context.Add(instituicao);
            //_context.SaveChanges();

            return instituicao;
        }

        private Curso CriarCurso(Instituicao instituicao)
        {
            if (!_env.IsDevelopment()) return null;

            var descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";

            var curso = new Curso("Análise e Desenvolvimento de Sistemas", "ADS", descricao, instituicao.Id);

            if (_context.Cursos.Any(i => i.NomePesquisa == curso.NomePesquisa))
                return null;

            _context.Add(curso);
            //_context.SaveChanges();

            return curso;
        }

        private Turma CriarTurma(Curso curso, Professor professor)
        {
            if (!_env.IsDevelopment()) return null;

            var descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";

            var turma = new Turma("6ª Turma ADS - 1º Semestre", descricao, "Algoritmos", curso.Id, professor.Id);

            if (_context.Cursos.Any(i => i.NomePesquisa == turma.NomePesquisa))
                return null;

            _context.Add(turma);
            //_context.SaveChanges();

            return turma;
        }



    }
}
