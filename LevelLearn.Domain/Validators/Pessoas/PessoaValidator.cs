using FluentValidation;
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
                    .WithMessage("")
                .Length(RegraAtributo.Pessoa.NOME_TAMANHO_MIN, RegraAtributo.Pessoa.NOME_TAMANHO_MAX)
                    .WithMessage("")
                .Must(n => TemPrimeiroNomeSobrenome(n))
                    .WithMessage("");
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
            var tamanhoMax = RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX;
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + tamanhoMax + "}$"; //^[a-zA-Z][A-Za-z0-9_\-\.]*$

            RuleFor(p => p.NickName)
                .NotEmpty().WithMessage("")
                .Must(p => Regex.IsMatch(p, pattern))
                    .WithMessage("")
                .MaximumLength(tamanhoMax)
                    .WithMessage("");
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty().WithMessage("");
        }       

        private void ValidarDataNascimento()
        {
            var dataAtual = DateTime.Now.Date;

            RuleFor(c => c.DataNascimento)
                .LessThan(dataAtual)
                    .WithMessage("")
                .When(p => p.DataNascimento.HasValue);
        }

        private void ValidarGenero()
        {
            RuleFor(p => p.Genero)
                .Must(c => c != Generos.Nenhum)
                    .WithMessage("");
        }

        private void ValidarTipoPessoa()
        {
            RuleFor(p => p.TipoPessoa)
                .Must(c => c != TiposPessoa.Nenhum)
                .WithMessage("");
        }


    }
}
