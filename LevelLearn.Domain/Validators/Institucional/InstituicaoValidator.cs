using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class InstituicaoValidator : AbstractValidator<Instituicao>
    {
        public InstituicaoValidator()
        {
            ValidarNome();
            ValidarDescricao();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome precisa estar preenchido")
                .Length(PropertiesConfig.Instituicao.NOME_TAMANHO_MIN, PropertiesConfig.Pessoa.NOME_TAMANHO_MAX)
                .WithMessage($"O Nome precisa ter entre {PropertiesConfig.Instituicao.NOME_TAMANHO_MIN} e {PropertiesConfig.Instituicao.NOME_TAMANHO_MAX} caracteres");
        }

        private void ValidarDescricao()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("A Descrição precisa estar preenchida")
                .MaximumLength(PropertiesConfig.Instituicao.DESCRICAO_TAMANHO_MAX)
                .WithMessage($"A Descrição precisa ter no máximo {PropertiesConfig.Instituicao.DESCRICAO_TAMANHO_MAX} caracteres");
        }


    }
}
