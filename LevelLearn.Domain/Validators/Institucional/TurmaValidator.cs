using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Resource.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        private readonly TurmaResource _resource = new TurmaResource();

        public TurmaValidator()
        {
            ValidarTurmaId();
            ValidarNome();
            ValidarNomeDisciplina();
            ValidarDescricao();
            ValidarNomePesquisa();
            ValidarProfessorId();
            ValidarCursoId();
        }

        private void ValidarTurmaId()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarNome()
        {
            int tamanhoMin = RegraTurma.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraTurma.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_resource.TurmaNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.TurmaNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarNomeDisciplina()
        {
            var tamanhoMin = RegraTurma.NOME_DISCIPLINA_TAMANHO_MIN;
            var tamanhoMax = RegraTurma.NOME_DISCIPLINA_TAMANHO_MAX;

            RuleFor(p => p.NomeDisciplina)
                .NotEmpty()
                    .WithMessage(_resource.TurmaNomeDisciplinaObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.TurmaNomeDisciplinaTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraTurma.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_resource.TurmaDescricaoObrigatoria)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.TurmaDescricaoTamanhoMaximo(tamanhoMax));
        }

        private void ValidarNomePesquisa()
        {
            RuleFor(p => p.NomePesquisa)
                .NotEmpty().WithMessage(_resource.NomePesquisaObrigatorio());
        }

        private void ValidarCursoId()
        {
            RuleFor(p => p.CursoId)
                .NotEmpty().WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarProfessorId()
        {
            RuleFor(p => p.ProfessorId)
                .NotEmpty().WithMessage(_resource.IdObrigatorio());
        }

    }
}
