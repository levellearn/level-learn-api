using FluentValidation;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource.Usuarios;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class EmailValidator : AbstractValidator<Email>
    {
        private readonly UsuarioResource _resource;

        public EmailValidator()
        {
            _resource = UsuarioResource.ObterInstancia();

            var tamanhoMax = RegraUsuario.EMAIL_TAMANHO_MAX;

            RuleFor(e => e.Endereco)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioEmailObrigatorio)
                .MaximumLength(RegraUsuario.EMAIL_TAMANHO_MAX)
                    .WithMessage(_resource.UsuarioEmailTamanhoMaximo(tamanhoMax))
                .EmailAddress()
                    .WithMessage(_resource.UsuarioEmailInvalido)
                .OverridePropertyName("Email");
        }

    }
}
