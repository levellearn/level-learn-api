using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class InstituicaoValidator : AbstractValidator<Instituicao>, IValidatorApp<Instituicao>
    {
        #region Ctors
        private readonly ISharedResource _sharedResource;

        // Unit Test
        public InstituicaoValidator()
        {
            _sharedResource = new SharedResource();
        }

        public InstituicaoValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        } 
        #endregion

        public ValidationResult Validar(Instituicao instance)
        {
            ValidarNome();
            ValidarDescricao();

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraAtributo.Instituicao.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Instituicao.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage(_sharedResource.InstituicaoNomeObrigatorio)
            .Length(tamanhoMin, RegraAtributo.Pessoa.NOME_TAMANHO_MAX)
                .WithMessage(_sharedResource.InstituicaoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_sharedResource.InstituicaoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_sharedResource.InstituicaoDescricaoTamanho(tamanhoMax));
        }

    }
}
