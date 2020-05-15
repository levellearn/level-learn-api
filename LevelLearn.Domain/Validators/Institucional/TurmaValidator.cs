using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        public TurmaValidator()
        {
            //ValidarTurmaId();
            ValidarNome();
            ValidarNomeDisciplina();
            ValidarDescricao();
        }

        //private void ValidarTurmaId()
        //{
        //    RuleFor(p => p.Id)
        //        .NotEmpty()
        //            .WithMessage(_sharedResource.IdObrigatorio);
        //}

        private void ValidarNome()
        {
            int tamanhoMin = RegraAtributo.Turma.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraAtributo.Turma.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("Nome precisa estar preenchido")
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage($"Nome precisa estar entre {tamanhoMin} e {tamanhoMax} caracteres");
        }

        private void ValidarNomeDisciplina()
        {
            var tamanhoMin = RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX;

            RuleFor(p => p.NomeDisciplina)
                .NotEmpty()
                    .WithMessage("Nome da disciplina precisa estar preenchido")
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage($"Nome da disciplina precisa estar entre {tamanhoMin} e {tamanhoMax} caracteres");
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage("Descrição precisa estar preenchida")
                .MaximumLength(tamanhoMax)
                    .WithMessage($"Descrição pode ter no máximo {tamanhoMax} caracteres");
        }


    }
}
