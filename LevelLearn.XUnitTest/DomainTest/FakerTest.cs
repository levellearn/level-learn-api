using Bogus;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using Bogus.Extensions.Brazil;

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
            var celular = "(12)98845-7832";
            var ra = "f1310435";
            var dataNascimento = DateTime.Parse("26/10/1993");

            return new Aluno(nome, new CPF(cpf), new Celular(celular), ra,
                genero, dataNascimento);
        }
        #endregion




    }
}
