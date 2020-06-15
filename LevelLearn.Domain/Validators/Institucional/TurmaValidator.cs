using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators.RegrasAtributos;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        // TODO: Implementar
        //private readonly TurmaResource _resource;

        public TurmaValidator()
        {
            //ValidarTurmaId();
            ValidarNome();
            ValidarNomeDisciplina();
            ValidarDescricao();
            //ValidarNomePesquisa();
        }

        //private void ValidarTurmaId()
        //{
        //    RuleFor(p => p.Id)
        //        .NotEmpty()
        //            .WithMessage(_resource.IdObrigatorio());
        //}

        private void ValidarNome()
        {
            int tamanhoMin = RegraTurma.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraTurma.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("Nome precisa estar preenchido")
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage($"Nome precisa estar entre {tamanhoMin} e {tamanhoMax} caracteres");
        }

        private void ValidarNomeDisciplina()
        {
            var tamanhoMin = RegraTurma.NOME_DISCIPLINA_TAMANHO_MIN;
            var tamanhoMax = RegraTurma.NOME_DISCIPLINA_TAMANHO_MAX;

            RuleFor(p => p.NomeDisciplina)
                .NotEmpty()
                    .WithMessage("Nome da disciplina precisa estar preenchido")
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage($"Nome da disciplina precisa estar entre {tamanhoMin} e {tamanhoMax} caracteres");
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraTurma.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage("Descrição precisa estar preenchida")
                .MaximumLength(tamanhoMax)
                    .WithMessage($"Descrição pode ter no máximo {tamanhoMax} caracteres");
        }

        //private void ValidarNomePesquisa()
        //{
        //    RuleFor(p => p.NomePesquisa)
        //        .NotEmpty()
        //            .WithMessage(_resource.NomePesquisaObrigatorio());
        //}


    }
}
