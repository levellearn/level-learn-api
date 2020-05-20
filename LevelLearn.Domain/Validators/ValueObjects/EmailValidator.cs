using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;
using LevelLearn.Resource.Usuarios;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class EmailValidator : AbstractValidator<Email>, IValidador<Email>
    {
        private readonly UsuarioResource _resource;

        public EmailValidator(ISharedResource sharedResource)
        {
            _resource = new UsuarioResource();
        }

        public ValidationResult Validar(Email instance)
        {
            var tamanhoMax = RegraAtributo.Usuario.EMAIL_TAMANHO_MAX;

            RuleFor(e => e.Endereco)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioEmailObrigatorio)
                .MaximumLength(RegraAtributo.Usuario.EMAIL_TAMANHO_MAX)
                    .WithMessage(_resource.UsuarioEmailTamanhoMaximo(tamanhoMax))
                .EmailAddress()
                    .WithMessage(_resource.UsuarioEmailInvalido)
                .OverridePropertyName("Email");

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }
    }
}
