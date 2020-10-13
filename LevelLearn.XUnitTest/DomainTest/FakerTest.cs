using Bogus;
using Bogus.Extensions.Brazil;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.XUnitTest.DomainTest
{
    public static class FakerTest
    {
        #region Alunos
        public static Aluno CriarAlunoFakeValido()
        {
            var genero = new Faker().PickRandom(GeneroPessoa.Feminino, GeneroPessoa.Masculino, GeneroPessoa.Outros);
            var aluno = new Faker<Aluno>(locale: "pt_BR")
                .CustomInstantiator(f => new Aluno(
                    f.Name.FullName(),
                    f.Person.Cpf(),
                    f.Phone.PhoneNumber("+## ## #####-####"),
                    "f1220202",
                    genero,
                    f.Date.Past(80, DateTime.Now.AddYears(-15))
                    ));

            return aluno;
        }

        public static Aluno CriarAlunoPadrao()
        {
            var nome = "Felipe Ayres";
            var cpf = "881.192.990-35";
            var genero = GeneroPessoa.Masculino;
            var celular = "55(12)98845-7832";
            var ra = "f1310435";
            var dataNascimento = DateTime.Parse("26/10/1993");

            return new Aluno(nome, new CPF(cpf), new Celular(celular), ra,
                genero, dataNascimento);
        }
        #endregion

        #region Professor

        public static Professor CriarProfessorFakeValido()
        {
            var genero = new Faker().PickRandom(GeneroPessoa.Feminino, GeneroPessoa.Masculino, GeneroPessoa.Outros);
            var professor = new Faker<Professor>(locale: "pt_BR")
                .CustomInstantiator(f => new Professor(
                    f.Name.FullName(),
                    f.Person.Cpf(),
                    f.Phone.PhoneNumber("+## ## #####-####"),
                    genero,
                    f.Date.Past(80, DateTime.Now.AddYears(-21))
                    ));

            return professor;
        }

        public static Professor CriarProfessorPadrao()
        {
            var nome = "Leandro Guarino";
            var cpf = "881.192.990-35";
            var genero = GeneroPessoa.Masculino;
            var celular = "55(12)98845-8974";
            var dataNascimento = DateTime.Parse("30/12/1988");

            return new Professor(nome, new CPF(cpf), new Celular(celular),
                genero, dataNascimento);
        }
        #endregion

        #region Instituicao
        public static Instituicao CriarInstituicaoPadrao()
        {
            var nome = "FATEC Guaratinguetá";
            var descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";
            var sigla = "FATEC GT";
            var cnpj = "62823257000109";
            var cep = "12517010";
            var municipio = "GUARATINGUETA";
            var uf = "SP";
            var organizacaoAcademica = OrganizacaoAcademica.Faculdade;
            var rede = Rede.Publica;
            var categoriaAdministrativa = CategoriaAdministrativa.Estadual;
            var nivelEnsino = NivelEnsino.Superior;

            var instituicao = new Instituicao(nome, sigla, descricao, cnpj, organizacaoAcademica, rede, categoriaAdministrativa, nivelEnsino, cep, municipio, uf);

            var aluno = CriarAlunoFakeValido();
            var professor = CriarProfessorFakeValido();

            var professorAdminInstituicao = new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, professor.Id, instituicao.Id)
            {
                Pessoa = professor,
                Instituicao = instituicao
            };
            var alunoInstituicao = new PessoaInstituicao(PerfilInstituicao.Aluno, aluno.Id, instituicao.Id)
            {
                Pessoa = aluno,
                Instituicao = instituicao
            };

            instituicao.AtribuirPessoa(professorAdminInstituicao);
            instituicao.AtribuirPessoa(alunoInstituicao);

            return instituicao;
        }
        #endregion

        #region Turma
        public static Turma CriarTurmaPadrao()
        {
            var curso =  CriarCursoPadrao();

            var nome = "6ª Turma ADS - 1º Semestre - ALGORITMOS";
            var descricao = "Analisar problemas e projetar, validar soluções computacionais para os mesmos, através do uso de metodologias, técnicas e ferramentas de programação envolvendo elementos básicos da construção de algoritmos e programas de computador.";
            var nomeDisciplina = "ALGORITMOS";

            Pessoa professor = curso.Pessoas.First(p => p.Perfil == TipoPessoa.Professor).Pessoa;
            Pessoa aluno = curso.Pessoas.First(p => p.Perfil == TipoPessoa.Aluno).Pessoa;

            var turma = new Turma(nome, descricao, nomeDisciplina, curso.Id, professor.Id);
            var alunoTurma = new AlunoTurma(aluno.Id, turma.Id) { Aluno = aluno, Turma = turma };
            turma.AtribuirAluno(alunoTurma);

            return turma;
        }

        public static IEnumerable<Turma> CriarListaTurmaFake(int quantidade)
        {
            var curso =  CriarCursoPadrao();
            Pessoa professor = curso.Pessoas.First(p => p.Perfil == TipoPessoa.Professor).Pessoa;
            var turmas = new Faker<Turma>(locale: "pt_BR")
                .CustomInstantiator(f => new Turma(f.Company.CompanyName(), f.Lorem.Sentence(10), f.Lorem.Sentence(2), curso.Id, professor.Id));

            return turmas.Generate(quantidade);
        }
        #endregion

        #region Curso
        public static Curso CriarCursoPadrao()
        {
            var instituicao = CriarInstituicaoPadrao();

            var nome = "Análise e Desenvolvimento de Sistemas";
            var sigla = "ADS";
            var descricao = "O curso forma o tecnólogo que analisa, projeta, documenta, especifica, testa, implanta e mantém sistemas computacionais de informação. Esse profissional trabalha, também, com ferramentas computacionais, equipamentos de informática e metodologia de projetos na produção de sistemas. Raciocínio lógico, emprego de linguagens de programação e de metodologias de construção de projetos, preocupação com a qualidade, usabilidade, integridade e segurança de programas computacionais são fundamentais à atuação desse profissional.";

            var curso = new Curso(nome, sigla, descricao, instituicao.Id);

            var professor = instituicao.Pessoas.First(p => p.Perfil == PerfilInstituicao.ProfessorAdmin).Pessoa;
            var aluno = instituicao.Pessoas.First(p => p.Perfil == PerfilInstituicao.Aluno).Pessoa;

            var professorCurso = new PessoaCurso(TipoPessoa.Professor, professor.Id, curso.Id)
            {
                Pessoa = professor,
                Curso = curso
            };
            var alunoCurso = new PessoaCurso(TipoPessoa.Aluno, aluno.Id, curso.Id)
            {
                Pessoa = professor,
                Curso = curso
            };

            curso.AtribuirPessoa(professorCurso);
            curso.AtribuirPessoa(alunoCurso);

            instituicao.AtribuirCurso(curso);

            return curso;
        }
        #endregion

        #region Usuario
        public static Usuario CriarUsuarioPadrao()
        {
            var nome = "Felipe Ayres";
            var email = "felipe.ayres93@gmail.com";
            var celular = "55(12)98845-7832";
            var nickName = "felipe.ayres";
            var senha = "Gamificando@123";
            var confirmacaoSenha = "Gamificando@123";

            var pessoa = new Professor(nome, new CPF("226.547.010-42"),
               new Celular(celular), GeneroPessoa.Masculino, DateTime.Parse("1993-10-26"));

            var user = new Usuario(nome, nickName, email, celular, senha, confirmacaoSenha);
            user.AtribuirPessoaId(pessoa.Id);
            user.ConfirmarEmail();
            user.ConfirmarCelular();

            return user;
        }
        #endregion



    }
}
