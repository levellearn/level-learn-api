using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Resource.Resources;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class InstituicaoValidator : AbstractValidator<Instituicao>
    {
        private readonly InstituicaoResource _resource;

        public InstituicaoValidator()
        {
            _resource = new InstituicaoResource();

            ValidarInstituicaoId();
            ValidarNome();
            ValidarDescricao();
        }

        private void ValidarInstituicaoId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoIdObrigatorio);
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraAtributo.Instituicao.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Instituicao.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage(_resource.InstituicaoNomeObrigatorio)
            .Length(tamanhoMin, tamanhoMax)
                .WithMessage(_resource.InstituicaoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.InstituicaoDescricaoTamanho(tamanhoMax));
        }

    }
}
