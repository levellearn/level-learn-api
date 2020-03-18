using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public CursoValidator()
        {
            ValidarNome();
            ValidarSigla();
            ValidarDescricao();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Nome precisa estar preenchido")
                .Length(PropertiesConfig.Curso.NOME_TAMANHO_MIN, PropertiesConfig.Curso.NOME_TAMANHO_MAX)
                .WithMessage($"Nome precisa estar entre {PropertiesConfig.Curso.NOME_TAMANHO_MIN} e {PropertiesConfig.Curso.NOME_TAMANHO_MAX} caracteres");
        }

        private void ValidarSigla()
        {
            RuleFor(p => p.Sigla)
                .NotEmpty().WithMessage("Sigla precisa estar preenchida")
                .Length(PropertiesConfig.Curso.SIGLA_TAMANHO_MIN, PropertiesConfig.Curso.SIGLA_TAMANHO_MAX)
                .WithMessage($"Sigla precisa estar entre {PropertiesConfig.Curso.SIGLA_TAMANHO_MIN} e {PropertiesConfig.Curso.SIGLA_TAMANHO_MAX} caracteres");
        }

        private void ValidarDescricao()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Descrição precisa estar preenchida")
                .MaximumLength(PropertiesConfig.Curso.DESCRICAO_TAMANHO_MAX)
                .WithMessage($"Descrição pode ter no máximo {PropertiesConfig.Curso.DESCRICAO_TAMANHO_MAX} caracteres");
        }


    }
}
