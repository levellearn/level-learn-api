﻿using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        public TurmaValidator()
        {
            ValidarNome();
            ValidarDescricao();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome precisa estar preenchido")
                .Length(PropertiesConfig.Turma.NOME_TAMANHO_MIN, PropertiesConfig.Turma.NOME_TAMANHO_MAX)
                .WithMessage($"O Nome precisa ter entre {PropertiesConfig.Turma.NOME_TAMANHO_MIN} e {PropertiesConfig.Turma.NOME_TAMANHO_MAX} caracteres");
        }

        private void ValidarDescricao()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("A Descrição precisa estar preenchida")
                .MaximumLength(PropertiesConfig.Turma.DESCRICAO_TAMANHO_MAX)
                .WithMessage($"A Descrição precisa ter no máximo {PropertiesConfig.Turma.DESCRICAO_TAMANHO_MAX} caracteres");
        }


    }
}