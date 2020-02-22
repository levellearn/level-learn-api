using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using NUnit.Framework;
using System;

namespace LevelLearn.NUnitTest.Institucional
{
    [TestFixture]
    public class InstituicaoTest
    {
        #region Fields
        private string _nome, _descricao;
        #endregion

        [SetUp]
        public void Setup()
        {
            _nome = "FATEC Guaratinguetá";
            _descricao = "Autarquia do Governo do Estado de São Paulo vinculada à Secretaria de Desenvolvimento Econômico, Ciência e Tecnologia, o Centro Paula Souza administra 220 Escolas Técnicas (Etecs) e 66 Faculdades de Tecnologia (Fatecs) estaduais em 162 municípios paulistas.";
        }

        [Test]
        public void Cadastrar_InstituicaoValida_ReturnTrue()
        {
            var instituicao = CriarInstituicao();

            bool valido = instituicao.EstaValido();
            Assert.IsTrue(valido, "Instituição deveria ser válida");
        }

        [Test]
        [TestCase("UniFatea", "")]
        [TestCase("", "Descrição de teste")]
        public void Cadastrar_InstituicaoValida_ReturnFalse(string nome, string descricao)
        {
            _nome = nome;
            _descricao = descricao;

            var instituicao = CriarInstituicao();
            bool valido = instituicao.EstaValido();
            Assert.IsFalse(valido, "Instituição deveria ser inválida");
        }

        private Instituicao CriarInstituicao()
        {
            var instituicao = new Instituicao(_nome, _descricao);

            var nome = "Felipe Ayres";
            var userName = "felipe_ayres";
            var email = "felipe.ayres@mail.com";
            var cpf = "881.192.990-35";
            var genero = Generos.Masculino;
            var celular = "(12)98845-7832";
            var ra = "f1310513";
            var imagemUrl = "https://firebasestorage.googleapis.com/v0/b/level-learn.appspot.com/o/Imagens/foto-default";
            var dataNascimento = DateTime.Parse("26/10/1993");

            var aluno = new Aluno(nome, userName, new Email(email), new CPF(cpf), new Celular(celular), ra, genero, imagemUrl, dataNascimento);

            var pessoaInstituicao = 
                new PessoaInstituicao(PerfisInstituicao.Aluno, aluno.Id, instituicao.Id);

            instituicao.AtribuirPessoa(pessoaInstituicao);

            return instituicao;
        }
    }
}
