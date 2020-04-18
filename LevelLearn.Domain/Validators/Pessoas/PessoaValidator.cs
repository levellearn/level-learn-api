using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.ValueObjects;
using LevelLearn.Resource;
using System;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class PessoaValidator : AbstractValidator<Pessoa>, IValidatorApp<Pessoa>
    {
        private readonly ISharedResource _sharedResource;

        // Unit Test
        public PessoaValidator()
        {
            _sharedResource = new SharedResource();
        }

        public PessoaValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }      

        public ValidationResult Validar(Pessoa instance)
        {
            // Pessoa
            ValidarNome();
            ValidarNickName();
            ValidarDataNascimento();
            ValidarGenero();
            ValidarTipoPessoa();

            instance.ResultadoValidacao = this.Validate(instance);

            // VOs            
            ValidarCPF(instance);
            ValidarEmail(instance);
            ValidarCelular(instance);           

            return instance.ResultadoValidacao;
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraAtributo.Pessoa.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Pessoa.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_sharedResource.PessoaNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_sharedResource.PessoaNomeTamanho(tamanhoMin, tamanhoMax))
                .Must(n => TemPrimeiroNomeSobrenome(n))
                    .WithMessage(_sharedResource.PessoaNomePrecisaSobrenome);
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
                .NotEmpty()
                    .WithMessage(_sharedResource.PessoaNickNameObrigatorio)
                .Must(p => Regex.IsMatch(p, pattern))
                    .WithMessage(_sharedResource.PessoaNickNameInvalido)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_sharedResource.PessoaNickNameTamanhoMaximo(tamanhoMax));
        }        

        private void ValidarDataNascimento()
        {
            var dataAtual = DateTime.Now.Date;

            RuleFor(c => c.DataNascimento)
                .LessThan(dataAtual)
                    .WithMessage(_sharedResource.PessoaDataNascimentoInvalida)
                .When(p => p.DataNascimento.HasValue);
        }

        private void ValidarGenero()
        {
            RuleFor(p => p.Genero)
                .Must(c => c != Generos.Nenhum)
                    .WithMessage(_sharedResource.PessoaGeneroObrigatorio);
        }

        private void ValidarTipoPessoa()
        {
            RuleFor(p => p.TipoPessoa)
                .Must(c => c != TiposPessoa.Nenhum)
                    .WithMessage(_sharedResource.PessoaTipoPessoaInvalido);
        }

        protected void ValidarCPF(Pessoa instance)
        {
            var cpfValidator = new CPFValidator(_sharedResource);
            var cpfResultadoValidacao = cpfValidator.Validar(instance.Cpf);

            if (!cpfResultadoValidacao.IsValid)
                instance.ResultadoValidacao.AddErrors(cpfResultadoValidacao);
        }

        protected void ValidarEmail(Pessoa instance)
        {
            var emailValidator = new EmailValidator(_sharedResource);
            var emailValidatorResultadoValidacao = emailValidator.Validar(instance.Email);

            if (!emailValidatorResultadoValidacao.IsValid)
                instance.ResultadoValidacao.AddErrors(emailValidatorResultadoValidacao);
        }

        protected void ValidarCelular(Pessoa instance)
        {
            var celularValidator = new CelularValidator(_sharedResource);
            var celularResultadoValidacao = celularValidator.Validar(instance.Celular);

            if (!celularResultadoValidacao.IsValid)
                instance.ResultadoValidacao.AddErrors(celularResultadoValidacao);
        }

    }
}
