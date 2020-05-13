using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class ProfessorValidator : AbstractValidator<Professor>, IValidador<Professor>
    {
        #region Ctors
        private readonly ISharedResource _sharedResource;

        // Unit Test
        public ProfessorValidator()
        {
            _sharedResource = new SharedResource();
        }

        public ProfessorValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        } 
        #endregion

        public ValidationResult Validar(Professor instance)
        {
            var pessoaValidator = new PessoaValidator(_sharedResource);
            var pessoaResultadoValidacao = pessoaValidator.Validar(instance);

            ValidarDocumento();
            instance.ResultadoValidacao = this.Validate(instance);

            if (!pessoaResultadoValidacao.IsValid)
                instance.ResultadoValidacao.AddErrors(pessoaResultadoValidacao);

            return instance.ResultadoValidacao;
        }

        private void ValidarDocumento()
        {
            RuleFor(p => p.Cpf.Numero)
                .NotEmpty()
                    .WithMessage(_sharedResource.ProfessorCPFObrigatorio)
                .OverridePropertyName("CPF");
        }

       
    }
}
