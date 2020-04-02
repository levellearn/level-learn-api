using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        public TurmaValidator()
        {
            ValidarNome();
            ValidarNomeDisciplina();
            ValidarDescricao();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("Nome precisa estar preenchido")
                .Length(RegraAtributo.Turma.NOME_TAMANHO_MIN, RegraAtributo.Turma.NOME_TAMANHO_MAX)
                    .WithMessage($"Nome precisa estar entre {RegraAtributo.Turma.NOME_TAMANHO_MIN} e {RegraAtributo.Turma.NOME_TAMANHO_MAX} caracteres");
        }

        private void ValidarNomeDisciplina()
        {
            RuleFor(p => p.NomeDisciplina)
                .NotEmpty()
                    .WithMessage("Nome da disciplina precisa estar preenchido")
                .Length(RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MIN, RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX)
                    .WithMessage($"Nome da disciplina precisa estar entre {RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MIN} e {RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX} caracteres");
        }

        private void ValidarDescricao()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage("Descrição precisa estar preenchida")
                .MaximumLength(RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX)
                    .WithMessage($"Descrição pode ter no máximo {RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX} caracteres");
        }


    }
}
