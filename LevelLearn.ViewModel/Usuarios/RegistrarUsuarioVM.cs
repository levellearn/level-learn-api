using System;

namespace LevelLearn.ViewModel.Usuarios
{
    public class RegistrarUsuarioVM
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        //[Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmacaoSenha { get; set; }

        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
