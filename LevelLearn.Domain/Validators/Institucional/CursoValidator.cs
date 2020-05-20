using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Resource.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        private readonly CursoResource _resource;

        public CursoValidator()
        {
            _resource = new CursoResource();

            ValidarCursoId();
            ValidarNome();
            ValidarSigla();
            ValidarDescricao();
            ValidarInstituicaoId();
        }

        private void ValidarCursoId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio);
        }

        private void ValidarNome()
        {
            int tamanhoMin = RegraAtributo.Curso.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraAtributo.Curso.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_resource.CursoNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.CursoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarSigla()
        {
            int tamanhoMin = RegraAtributo.Curso.SIGLA_TAMANHO_MIN;
            int tamanhoMax = RegraAtributo.Curso.SIGLA_TAMANHO_MAX;

            RuleFor(p => p.Sigla)
                .NotEmpty()
                    .WithMessage(_resource.CursoSiglaObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                .WithMessage(_resource.CursoSiglaTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Curso.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_resource.CursoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.CursoDescricaoTamanho(tamanhoMax));
        }

        private void ValidarInstituicaoId()
        {
            RuleFor(p => p.InstituicaoId)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio);
        }

    }
}
