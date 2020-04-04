using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
        {
            ValidarDocumento();
        }

        private void ValidarDocumento()
        {
            RuleFor(p => p.Cpf.Numero)
                .NotEmpty()
                    .WithMessage("");
        }


    }
}
