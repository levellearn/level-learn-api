using FluentValidation;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Resource.Usuarios;
using System.Text.RegularExpressions;

namespace LevelLearn.ViewModel.Usuarios.Validators
{
    public class RedefinirSenhaVMValidator : AbstractValidator<RedefinirSenhaVM>
    {
        private readonly UsuarioResource _resource;

        public RedefinirSenhaVMValidator()
        {
            _resource = UsuarioResource.ObterInstancia();

            ValidarId();
            ValidarToken();
            ValidarSenha();
            ValidarConfirmacaoSenha();
        }

        private void ValidarId()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarToken()
        {
            RuleFor(p => p.Token)
                .NotEmpty().WithMessage(_resource.TokenObrigatorio());
        }

        private void ValidarSenha()
        {
            var tamanhoMin = RegraUsuario.SENHA_TAMANHO_MIN;
            var tamanhoMax = RegraUsuario.SENHA_TAMANHO_MAX;

            RuleFor(p => p.NovaSenha)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioSenhaObrigatoria)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.UsuarioSenhaTamanho(tamanhoMin, tamanhoMax))
                .Must(p => Regex.IsMatch(p, "[A-Z]") || RegraUsuario.SENHA_REQUER_MAIUSCULO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerMaiusculo)
                .Must(p => Regex.IsMatch(p, "[a-z]") || RegraUsuario.SENHA_REQUER_MINUSCULO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerMinusculo)
                .Must(p => Regex.IsMatch(p, "[0-9]") || RegraUsuario.SENHA_REQUER_DIGITO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerDigito)
                .Must(p => Regex.IsMatch(p, "[^a-zA-Z0-9]") || RegraUsuario.SENHA_REQUER_ESPECIAL == false)
                    .WithMessage(_resource.UsuarioSenhaRequerEspecial);
        }

        private void ValidarConfirmacaoSenha()
        {
            RuleFor(p => p.ConfirmacaoSenha)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioConfirmacaoSenhaObrigatoria)
                .Equal(p => p.NovaSenha)
                    .WithMessage(_resource.UsuarioConfirmacaoSenhaNaoConfere);
        }


    }
}
