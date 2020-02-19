using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {            
            ValidarRA();
        }

        private void ValidarRA()
        {
            RuleFor(p => p.RA)
                .NotEmpty().WithMessage("O RA precisa estar preenchido");
        }


    }
}
