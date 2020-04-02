using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class InstituicaoValidator : AbstractValidator<Instituicao>, IInstituicaoValidator
    {
        private readonly ISharedResource _sharedLocalizer;

        public InstituicaoValidator(ISharedResource sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public ValidationResult Validar(Instituicao instance)
        {
            ValidarNome();
            ValidarDescricao();

            instance.ValidationResult = this.Validate(instance);

            return instance.ValidationResult;
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraAtributo.Instituicao.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Instituicao.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage(p => _sharedLocalizer.InstitutionNameRequired)
            .Length(tamanhoMin, RegraAtributo.Pessoa.NOME_TAMANHO_MAX)
                .WithMessage(p => _sharedLocalizer.InstitutionNameLength(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(p => _sharedLocalizer.InstitutionDescriptionRequired)
                .MaximumLength(tamanhoMax)
                    .WithMessage(p => _sharedLocalizer.InstitutionDescriptionLength(tamanhoMax));
        }

    }
}
