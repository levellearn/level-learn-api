using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        private readonly PessoaResource _resource;

        public AlunoValidator()
        {
            _resource = new PessoaResource();

            ValidarRA();
        }

        private void ValidarRA()
        {
            RuleFor(p => p.RA)
                .NotEmpty()
                    .WithMessage(_resource.AlunoRAObrigatorio);
        }


    }
}
