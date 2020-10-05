using Bogus;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using Bogus.Extensions.Brazil;
using LevelLearn.Domain.Entities.Institucional;

namespace LevelLearn.XUnitTest.DomainTest
{
    public static class FakerTest
    {
        #region Alunos
        public static Aluno CriarAlunoFakeValido()
        {
            var genero = new Faker().PickRandom<GeneroPessoa>();
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
            var genero = new Faker().PickRandom<GeneroPessoa>();
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



    }
}
