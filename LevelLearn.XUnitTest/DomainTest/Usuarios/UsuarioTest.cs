using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace LevelLearn.XUnitTest.DomainTest.Usuarios
{
    public class UsuarioTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private string _nome, _email, _nickName, _celular, _senha, _confirmacaoSenha;

        public UsuarioTest()
        {
            _nome = "Felipe Ayres";
            _nickName = "felipe.ayres";
            _email = "felipe.ayres@mail.com";
            _celular = "(12)98845-7832";
            _senha = "Gamificando@123";
            _confirmacaoSenha = "Gamificando@123";
        }


        [Theory]
        [InlineData("billGates3")]
        [InlineData("shaq_O-Neal")]
        [InlineData("steven.jobs")]
        [Trait("Categoria", "Usuarios")]
        public void Usuario_Nickname_ReturnTrue(string userName)
        {
            _nickName = userName;
            var usuario = CriarUsuario();

            usuario.EstaValido();

            var erros = usuario.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.Propriedade == nameof(Usuario.NickName));

            Assert.True(valido, "Usuário deveria ter NickName válido");
        }

        [Theory]
        [Trait("Categoria", "Usuarios")]
        [InlineData("bill@Gates3")]
        [InlineData("shaq$Neal")]
        [InlineData("#stevenjobs")]
        public void Usuario_Nickname_ReturnFalse(string userName)
        {
            _nickName = userName;
            var usuario = CriarUsuario();

            usuario.EstaValido();
            var erros = usuario.DadosInvalidos().ToList();
            bool valido = !erros.Exists(e => e.Propriedade == nameof(Usuario.NickName));

            Assert.False(valido, "Usuário deveria ter NickName inválido");
        }

        private Usuario CriarUsuario()
        {
            var pessoa = new Professor(_nome, new CPF("226.547.010-42"),
               new Celular(_celular), GeneroPessoa.Masculino, DateTime.Parse("1993-10-26"));

            var user = new Usuario(_nome, _nickName, _email, _celular, _senha, _confirmacaoSenha);
            user.AtribuirPessoaId(pessoa.Id);
            user.ConfirmarEmail();
            user.ConfirmarCelular();

            return user;
        }
    }
}
