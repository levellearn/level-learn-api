using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.ViewModel.Usuarios
{
    public class RegistrarProfessorVM
    {
        public string Nome { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public GeneroPessoa Genero { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
