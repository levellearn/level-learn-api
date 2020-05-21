using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace LevelLearn.NUnitTest.Usuarios
{
    [TestFixture]
    class UsuarioTest
    {
        // Fields
        private string _nome, _email, _nickName, _celular, _senha, _confirmacaoSenha;

        [SetUp]
        public void Setup()
        {
            _nome = "Felipe Ayres";
            _nickName = "felipe.ayres";
            _email = "felipe.ayres@mail.com";
            _celular = "(12)98845-7832";
            _senha = "Gamificando@123";
            _confirmacaoSenha = "Gamificando@123";
        }

        [Test]
        [TestCase("billGates3")]
        [TestCase("shaq_O-Neal")]
        [TestCase("steven.jobs")]
        public void Usuario_NicknameValido_ReturnTrue(string userName)
        {
            _nickName = userName;
            var usuario = CriarUsuario();

            usuario.EstaValido();

            var erros = usuario.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.Propriedade == nameof(Usuario.NickName));

            Assert.IsTrue(valido, "Usuário deveria ter NickName válido");
        }

        [Test]
        [TestCase("bill@Gates3")]
        [TestCase("shaq$Neal")]
        [TestCase("#stevenjobs")]
        public void Usuario_NicknameValido_ReturnFalse(string userName)
        {
            _nickName = userName;
            var usuario = CriarUsuario();

            usuario.EstaValido();
            var erros = usuario.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.Propriedade == nameof(Usuario.NickName));

            Assert.IsFalse(valido, "Usuário deveria ter NickName inválido");
        }

        private Usuario CriarUsuario()
        {
            var pessoa = new Professor(_nome, new Email(_email), new CPF("226.547.010-42"),
               new Celular(_celular), Generos.Masculino, DateTime.Parse("1993-10-26"));

            var user = new Usuario(_nome, _nickName, _email, _celular, pessoa.Id);
            user.AtribuirSenha(_senha, _confirmacaoSenha);
            user.ConfirmarEmail();
            user.ConfirmarCelular();

            return user;
        }

        public static Usuario CriarUsuarioPadrao()
        {
            var nome = "Felipe Ayres";
            var email = "felipe.ayres93@gmail.com";
            var celular = "(12)98845-7832";
            var nickName = "felipe.ayres";
            var senha = "Gamificando@123";
            var confirmacaoSenha = "Gamificando@123";

            var pessoa = new Professor(nome, new Email(email), new CPF("226.547.010-42"),
               new Celular(celular), Generos.Masculino, DateTime.Parse("1993-10-26"));

            var user = new Usuario(nome, nickName, email, celular, pessoa.Id);
            user.AtribuirSenha(senha, confirmacaoSenha);
            user.ConfirmarEmail();
            user.ConfirmarCelular();

            return user;
        }

    }
}
