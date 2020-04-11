using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.ValueObjects
{
    public class EmailValidator : AbstractValidator<Email>, IValidatorApp<Email>
    {
        private readonly ISharedResource _sharedResource;

        public EmailValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        public ValidationResult Validar(Email instance)
        {
            var tamanhoMax = RegraAtributo.Pessoa.EMAIL_TAMANHO_MAX;

            RuleFor(e => e.Endereco)
                .NotEmpty()
                    .WithMessage(_sharedResource.UsuarioEmailObrigatorio)
                .MaximumLength(RegraAtributo.Pessoa.EMAIL_TAMANHO_MAX)
                    .WithMessage(_sharedResource.UsuarioEmailTamanhoMaximo(tamanhoMax))
                .EmailAddress()
                    .WithMessage(_sharedResource.UsuarioEmailInvalido)
                .OverridePropertyName("Email");

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }
    }
}
