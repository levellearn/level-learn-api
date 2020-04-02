﻿using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using System;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            ValidarNome();
            ValidarNickName();
            ValidarDataNascimento();
            ValidarGenero();
            ValidarTipoPessoa();
            ValidarImagem();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("Nome precisa estar preenchido")
                .Length(RegraAtributo.Pessoa.NOME_TAMANHO_MIN, RegraAtributo.Pessoa.NOME_TAMANHO_MAX)
                    .WithMessage($"Nome precisa estar entre {RegraAtributo.Pessoa.NOME_TAMANHO_MIN} e {RegraAtributo.Pessoa.NOME_TAMANHO_MAX} caracteres")
                .Must(n => TemPrimeiroNomeSobrenome(n))
                    .WithMessage("Nome precisa de um sobrenome");
        }
        private bool TemPrimeiroNomeSobrenome(string name)
        {
            var pattern = @"^[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ'-]{3,}\s[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ\s\.'-]{2,}$";

            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                return true;
            else
                return false;
        }

        private void ValidarNickName()
        {
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX + "}$";

            RuleFor(p => p.NickName)
                .NotEmpty().WithMessage("NickName precisa estar preenchido")
                .Must(p => Regex.IsMatch(p, pattern))
                    .WithMessage("NickName somente deve conter letras, números, (_), (-) ou (.)")
                .MaximumLength(RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX)
                    .WithMessage($"NickName pode ter no máximo {RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX} caracteres");
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty().WithMessage("Imagem precisa estar preenchida");
        }       

        private void ValidarDataNascimento()
        {
            var dataAtual = DateTime.Now.Date;

            RuleFor(c => c.DataNascimento)
                .LessThan(dataAtual)
                    .WithMessage("Data Nascimento precisa ser menor que hoje")
                .When(p => p.DataNascimento.HasValue);
        }

        private void ValidarGenero()
        {
            RuleFor(p => p.Genero)
                .Must(c => c != Generos.Nenhum)
                    .WithMessage($"Gênero precisa ser informado");
        }

        private void ValidarTipoPessoa()
        {
            RuleFor(p => p.TipoPessoa)
                .Must(c => c != TiposPessoa.Nenhum)
                .WithMessage($"Tipo de pessoa precisa ser Admin, Professor ou Aluno");
        }


    }
}
