using FluentValidation;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource.Usuarios;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class SenhaValidator : AbstractValidator<Senha>
    {
        private readonly UsuarioResource _resource;

        public SenhaValidator()
        {
            _resource = UsuarioResource.ObterInstancia();

            int tamanhoMin = RegraUsuario.SENHA_TAMANHO_MIN;
            int tamanhoMax = RegraUsuario.SENHA_TAMANHO_MAX;

            RuleFor(p => p.Segredo)
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
                    .WithMessage(_resource.UsuarioSenhaRequerEspecial)
                .OverridePropertyName("Senha");
        }

    }
}
