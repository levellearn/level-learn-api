using FluentValidation.Results;
using LevelLearn.Domain.Entities.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public interface IInstituicaoValidator
    {
        ValidationResult Validar(Instituicao instance);
    }
}
