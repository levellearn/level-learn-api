using FluentValidation;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource.Usuarios;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class EmailValidator : AbstractValidator<Email>
    {
        private readonly UsuarioResource _resource;

        public EmailValidator()
        {
            _resource = new UsuarioResource();

            var tamanhoMax = RegraAtributo.Usuario.EMAIL_TAMANHO_MAX;

            RuleFor(e => e.Endereco)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioEmailObrigatorio)
                .MaximumLength(RegraAtributo.Usuario.EMAIL_TAMANHO_MAX)
                    .WithMessage(_resource.UsuarioEmailTamanhoMaximo(tamanhoMax))
                .EmailAddress()
                    .WithMessage(_resource.UsuarioEmailInvalido)
                .OverridePropertyName("Email");
        }

    }
}
