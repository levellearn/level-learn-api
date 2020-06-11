using FluentValidation;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Resource;
using System;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        private readonly PessoaResource _resource;

        public PessoaValidator()
        {
            _resource = PessoaResource.ObterInstancia();

            ValidarId();
            ValidarNome();
            ValidarDataNascimento();
            ValidarGenero();
            ValidarTipoPessoa();
            ValidarNomePesquisa();
        }

        private void ValidarId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraAtributo.Pessoa.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Pessoa.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_resource.PessoaNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.PessoaNomeTamanho(tamanhoMin, tamanhoMax))
                .Must(n => TemPrimeiroNomeSobrenome(n))
                    .WithMessage(_resource.PessoaNomePrecisaSobrenome);
        }
        private bool TemPrimeiroNomeSobrenome(string name)
        {
            var pattern = @"^[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ'-]{3,}\s[A-ZÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ðçÑ\s\.'-]{2,}$";

            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                return true;
            else
                return false;
        }

        private void ValidarDataNascimento()
        {
            DateTime dataAtual = DateTime.Now.Date;

            RuleFor(c => c.DataNascimento)
                .LessThan(dataAtual)
                    .WithMessage(_resource.PessoaDataNascimentoInvalida)
                .When(p => p.DataNascimento.HasValue);
        }

        private void ValidarGenero()
        {
            RuleFor(p => p.Genero)
                .Must(c => c != GeneroPessoa.Vazio)
                    .WithMessage(_resource.PessoaGeneroObrigatorio);
        }

        private void ValidarTipoPessoa()
        {
            RuleFor(p => p.TipoPessoa)
                .Must(c => c != TipoPessoa.Vazio)
                    .WithMessage(_resource.PessoaTipoPessoaInvalido);
        }

        private void ValidarNomePesquisa()
        {
            RuleFor(p => p.NomePesquisa)
                .NotEmpty()
                    .WithMessage(_resource.NomePesquisaObrigatorio());                
        }


    }
}
