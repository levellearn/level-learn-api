using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Resource;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Pessoas
{
    public class UsuarioValidator : AbstractValidator<ApplicationUser>, IValidatorApp<ApplicationUser>
    {
        private readonly ISharedResource _sharedResource;

        public UsuarioValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        public ValidationResult Validar(ApplicationUser instance)
        {
            ValidarSenha();
            ValidarConfirmacaoSenha();
            ValidarImagem();

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }

        private void ValidarSenha()
        {
            var tamanhoMin = RegraAtributo.Pessoa.SENHA_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Pessoa.SENHA_TAMANHO_MAX;

            RuleFor(p => p.Senha)
                .NotEmpty()
                    .WithMessage(_sharedResource.UsuarioSenhaObrigatoria)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_sharedResource.UsuarioSenhaTamanho(tamanhoMin, tamanhoMax))
                .Must(p => Regex.IsMatch(p, "[A-Z]") || RegraAtributo.Pessoa.SENHA_REQUER_MAIUSCULO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerMaiusculo)
                .Must(p => Regex.IsMatch(p, "[a-z]") || RegraAtributo.Pessoa.SENHA_REQUER_MINUSCULO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerMinusculo)
                .Must(p => Regex.IsMatch(p, "[0-9]") || RegraAtributo.Pessoa.SENHA_REQUER_DIGITO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerDigito)
                .Must(p => Regex.IsMatch(p, "[^a-zA-Z0-9]") || RegraAtributo.Pessoa.SENHA_REQUER_ESPECIAL == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerEspecial);
        }

        private void ValidarConfirmacaoSenha()
        {
            RuleFor(p => p.ConfirmacaoSenha)
                .NotEmpty()
                    .WithMessage(_sharedResource.UsuarioConfirmacaoSenhaObrigatoria)
                .Equal(p => p.Senha)
                    .WithMessage(_sharedResource.UsuarioConfirmacaoSenhaNaoConfere);
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty()
                    .WithMessage(_sharedResource.PessoaImagemObrigatoria);
        }

        //public DadoInvalido SenhaEstaValida(string senha)
        //{
        //    if (string.IsNullOrWhiteSpace(senha))
        //    {
        //        return new DadoInvalido("Senha", _sharedLocalizer.UsuarioSenhaObrigatoria);
        //    }
        //    var senhaTamanhoMin = RegraAtributo.Pessoa.SENHA_TAMANHO_MIN;
        //    var senhaTamanhoMax = RegraAtributo.Pessoa.SENHA_TAMANHO_MAX;

        //    if (senha.Length < senhaTamanhoMin || senha.Length > senhaTamanhoMax)
        //    {
        //        return new DadoInvalido("Senha", _sharedLocalizer.UsuarioSenhaTamanho(senhaTamanhoMin, senhaTamanhoMax));
        //    }

        //    return null;
        //}

    }
}
