using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class CursoValidator : AbstractValidator<Curso>, IValidador<Curso>
    {
        #region Ctors
        private readonly ISharedResource _sharedResource;

        // Unit Test
        public CursoValidator()
        {
            _sharedResource = new SharedResource();
        }

        public CursoValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }
        #endregion
        
        public ValidationResult Validar(Curso instance)
        {
            ValidarCursoId();
            ValidarNome();
            ValidarSigla();
            ValidarDescricao();
            ValidarInstituicaoId();

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }
        private void ValidarCursoId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_sharedResource.IdObrigatorio);
        }

        private void ValidarNome()
        {
            int tamanhoMin = RegraAtributo.Curso.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraAtributo.Curso.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_sharedResource.CursoNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_sharedResource.CursoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarSigla()
        {
            int tamanhoMin = RegraAtributo.Curso.SIGLA_TAMANHO_MIN;
            int tamanhoMax = RegraAtributo.Curso.SIGLA_TAMANHO_MAX;

            RuleFor(p => p.Sigla)
                .NotEmpty()
                    .WithMessage(_sharedResource.CursoSiglaObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                .WithMessage(_sharedResource.CursoSiglaTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Curso.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_sharedResource.CursoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_sharedResource.CursoDescricaoTamanho(tamanhoMax));
        }

        private void ValidarInstituicaoId()
        {
            RuleFor(p => p.InstituicaoId)
                .NotEmpty()
                    .WithMessage(_sharedResource.IdObrigatorio);                
        }

    }
}
