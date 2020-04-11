using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class AlunoValidator : AbstractValidator<Aluno>, IValidatorApp<Aluno>
    {
        #region Ctors
        private readonly ISharedResource _sharedResource;

        // Unit Test
        public AlunoValidator()
        {
            _sharedResource = new SharedResource();
        }

        public AlunoValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        } 
        #endregion

        public ValidationResult Validar(Aluno instance)
        {
            // Pessoa
            var pessoaValidator = new PessoaValidator(_sharedResource);
            var pessoaResultadoValidacao = pessoaValidator.Validar(instance);

            // Aluno
            ValidarRA();
            instance.ResultadoValidacao = this.Validate(instance);

            if (!pessoaResultadoValidacao.IsValid)
                instance.ResultadoValidacao.AddErrors(pessoaResultadoValidacao);

            return instance.ResultadoValidacao;
        }

        private void ValidarRA()
        {
            RuleFor(p => p.RA)
                .NotEmpty()
                    .WithMessage(_sharedResource.AlunoRAObrigatorio);
        }


    }
}
