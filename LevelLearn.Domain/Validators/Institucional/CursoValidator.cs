﻿using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Resource.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        private readonly CursoResource _resource;

        public CursoValidator()
        {
            _resource = new CursoResource();

            ValidarCursoId();
            ValidarNome();
            ValidarSigla();
            ValidarDescricao();
            ValidarInstituicaoId();
            ValidarNomePesquisa();
        }

        private void ValidarCursoId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarNome()
        {
            int tamanhoMin = RegraCurso.NOME_TAMANHO_MIN;
            int tamanhoMax = RegraCurso.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_resource.CursoNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.CursoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarSigla()
        {
            int tamanhoMin = RegraCurso.SIGLA_TAMANHO_MIN;
            int tamanhoMax = RegraCurso.SIGLA_TAMANHO_MAX;

            RuleFor(p => p.Sigla)
                .NotEmpty()
                    .WithMessage(_resource.CursoSiglaObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                .WithMessage(_resource.CursoSiglaTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraCurso.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                .NotEmpty()
                    .WithMessage(_resource.CursoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.CursoDescricaoTamanho(tamanhoMax));
        }

        private void ValidarInstituicaoId()
        {
            RuleFor(p => p.InstituicaoId)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarNomePesquisa()
        {
            RuleFor(p => p.NomePesquisa)
                .NotEmpty()
                    .WithMessage(_resource.NomePesquisaObrigatorio());
        }

    }
}
