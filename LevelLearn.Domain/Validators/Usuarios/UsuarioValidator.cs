using FluentValidation;
using FluentValidation.Results;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Resource;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class UsuarioValidator : AbstractValidator<Usuario>, IValidador<Usuario>
    {
        private readonly ISharedResource _sharedResource;

        #region Ctors

        // Unit Test
        public UsuarioValidator()
        {
            _sharedResource = new SharedResource();
        }

        public UsuarioValidator(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        } 

        #endregion

        public ValidationResult Validar(Usuario instance)
        {
            ValidarNickName();
            ValidarSenha();
            ValidarConfirmacaoSenha();
            ValidarImagem();
            ValidarPessoaId();

            instance.ResultadoValidacao = this.Validate(instance);

            return instance.ResultadoValidacao;
        }
        
        private void ValidarNickName()
        {
            //  Ex.: bill@GatesIII

            var tamanhoMax = RegraAtributo.Usuario.NICKNAME_TAMANHO_MAX;
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + tamanhoMax + "}$"; //^[a-zA-Z][A-Za-z0-9_\-\.]*$

            RuleFor(p => p.NickName)
                .NotEmpty()
                    .WithMessage(_sharedResource.PessoaNickNameObrigatorio)
                .Must(p => Regex.IsMatch(p, pattern))
                    .WithMessage(_sharedResource.PessoaNickNameInvalido)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_sharedResource.PessoaNickNameTamanhoMaximo(tamanhoMax));
        }

        private void ValidarSenha()
        {
            var tamanhoMin = RegraAtributo.Usuario.SENHA_TAMANHO_MIN;
            var tamanhoMax = RegraAtributo.Usuario.SENHA_TAMANHO_MAX;

            RuleFor(p => p.Senha)
                .NotEmpty()
                    .WithMessage(_sharedResource.UsuarioSenhaObrigatoria)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_sharedResource.UsuarioSenhaTamanho(tamanhoMin, tamanhoMax))
                .Must(p => Regex.IsMatch(p, "[A-Z]") || RegraAtributo.Usuario.SENHA_REQUER_MAIUSCULO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerMaiusculo)
                .Must(p => Regex.IsMatch(p, "[a-z]") || RegraAtributo.Usuario.SENHA_REQUER_MINUSCULO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerMinusculo)
                .Must(p => Regex.IsMatch(p, "[0-9]") || RegraAtributo.Usuario.SENHA_REQUER_DIGITO == false)
                    .WithMessage(_sharedResource.UsuarioSenhaRequerDigito)
                .Must(p => Regex.IsMatch(p, "[^a-zA-Z0-9]") || RegraAtributo.Usuario.SENHA_REQUER_ESPECIAL == false)
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

        private void ValidarPessoaId()
        {
            RuleFor(p => p.PessoaId)
                .NotEmpty()
                    .WithMessage(_sharedResource.IdObrigatorio);
        }



    }
}
