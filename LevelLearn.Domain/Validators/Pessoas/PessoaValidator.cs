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
            ValidarUserName();
            ValidarDataNascimento();
            ValidarGenero();
            ValidarTipoPessoa();
            ValidarImagem();
        }

        private void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome precisa estar preenchido")
                .Length(PropertiesConfig.Pessoa.NOME_TAMANHO_MIN, PropertiesConfig.Pessoa.NOME_TAMANHO_MAX)
                .WithMessage($"O Nome precisa ter entre {PropertiesConfig.Pessoa.NOME_TAMANHO_MIN} e {PropertiesConfig.Pessoa.NOME_TAMANHO_MAX} caracteres")
                .Must(n => IsFullName(n)).WithMessage("O Nome precisa de um sobrenome");
        }
        private bool IsFullName(string name)
        {
            var pattern = @"^[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ'-]{3,}\s[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ\s\.'-]{2,}$";

            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                return true;
            else
                return false;
        }

        private void ValidarUserName()
        {
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX + "}$";

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("O Username precisa estar preenchido")
                .Must(p => Regex.IsMatch(p, pattern))
                .WithMessage("O Username só deve conter letras, números, (_), (-) ou (.)")
                .MaximumLength(PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX)
                .WithMessage($"O Username precisa ter no máximo {PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX} caracteres");
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty().WithMessage("A Imagem precisa estar preenchida");
        }       

        private void ValidarDataNascimento()
        {
            var dataAtual = DateTime.Now.Date;

            RuleFor(c => c.DataNascimento)
                //.NotEmpty().WithMessage("A Data Nascimento precisa estar preenchida")
                .LessThan(dataAtual).WithMessage("A Data Nascimento precisa ser menor que hoje")
                .When(p => p.DataNascimento.HasValue);
        }

        private void ValidarGenero()
        {
            RuleFor(p => p.Genero)
                .NotEmpty().WithMessage("O Gênero precisa estar preenchido")
                .Must(c => c.Equals(Generos.Masculino) || c.Equals(Generos.Feminino))
                .WithMessage($"O Gênero precisa ser Masculino ou Feminino");
        }

        private void ValidarTipoPessoa()
        {
            RuleFor(p => p.TipoPessoa)
                .NotEmpty().WithMessage("O Tipo de pessoa precisa estar preenchido")
                .Must(c => c != TiposPessoa.Nenhum)
                .WithMessage($"O Tipo de pessoa precisa ser Admin, Professor ou Aluno");
        }


    }
}
