using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Resource;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        private readonly PessoaResource _resource;

        public ProfessorValidator()
        {
            _resource = PessoaResource.ObterInstancia();

            ValidarDocumento();
        }

        private void ValidarDocumento()
        {
            RuleFor(p => p.Cpf.Numero)
                .NotEmpty()
                    .WithMessage(_resource.ProfessorCPFObrigatorio)
                .OverridePropertyName("CPF");
        }


    }
}
