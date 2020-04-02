using FluentValidation;
using LevelLearn.Domain.Entities.Usuarios;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class UsuarioValidator : AbstractValidator<ApplicationUser>
    {
        public UsuarioValidator()
        {            
            ValidarSenha();
            ValidarConfirmacaoSenha();
        }
        
        private void ValidarSenha()
        {
            RuleFor(p => p.Senha)
                .NotEmpty()
                    .WithMessage("Senha precisa estar preenchida")
                .Length(RegraAtributo.Pessoa.SENHA_TAMANHO_MIN, RegraAtributo.Pessoa.SENHA_TAMANHO_MAX)
                    .WithMessage($"Senha precisa estar entre {RegraAtributo.Pessoa.SENHA_TAMANHO_MIN} e {RegraAtributo.Pessoa.SENHA_TAMANHO_MAX} caracteres")
                .Must(p => Regex.IsMatch(p, "[A-Z]") || RegraAtributo.Pessoa.SENHA_REQUER_MAIUSCULO == false)
                    .WithMessage("Senha precisa no mínimo de uma letra maiúscula")
                .Must(p => Regex.IsMatch(p, "[a-z]") || RegraAtributo.Pessoa.SENHA_REQUER_MINUSCULO == false)
                    .WithMessage("Senha precisa no mínimo de uma letra minúscula")
                .Must(p => Regex.IsMatch(p, "[0-9]") || RegraAtributo.Pessoa.SENHA_REQUER_DIGITO == false)
                    .WithMessage("Senha precisa no mínimo de um dígito")
                .Must(p => Regex.IsMatch(p, "[^a-zA-Z0-9]") || RegraAtributo.Pessoa.SENHA_REQUER_ESPECIAL == false)
                    .WithMessage("Senha precisa no mínimo de um caractere especial");
        }

        private void ValidarConfirmacaoSenha()
        {
            RuleFor(p => p.ConfirmacaoSenha)
                .NotEmpty()
                    .WithMessage("Confirmação de senha precisa estar preenchida")
                .Equal(p => p.Senha)
                    .WithMessage("Senha e confirmação de senha não conferem");
        }

    }
}
