using FluentValidation;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Resource.Usuarios;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Validators.Usuarios
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        private readonly UsuarioResource _resource;

        public UsuarioValidator()
        {
            _resource = UsuarioResource.ObterInstancia();

            ValidarNickName();
            ValidarSenha();
            ValidarConfirmacaoSenha();
            ValidarImagem();
            ValidarPessoaId();
        }

        private void ValidarNickName()
        {
            //  Ex.: bill@GatesIII

            var tamanhoMax = RegraUsuario.NICKNAME_TAMANHO_MAX;
            var pattern = @"^[A-Za-z0-9_\-\.]{1," + tamanhoMax + "}$"; //^[a-zA-Z][A-Za-z0-9_\-\.]*$

            RuleFor(p => p.NickName)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioNickNameObrigatorio)
                .Must(p => Regex.IsMatch(p, pattern))
                    .WithMessage(_resource.UsuarioNickNameInvalido)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.UsuarioNickNameTamanhoMaximo(tamanhoMax));
        }

        private void ValidarSenha()
        {
            var tamanhoMin = RegraUsuario.SENHA_TAMANHO_MIN;
            var tamanhoMax = RegraUsuario.SENHA_TAMANHO_MAX;

            RuleFor(p => p.Senha)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioSenhaObrigatoria)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.UsuarioSenhaTamanho(tamanhoMin, tamanhoMax))
                .Must(p => Regex.IsMatch(p, "[A-Z]") || RegraUsuario.SENHA_REQUER_MAIUSCULO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerMaiusculo)
                .Must(p => Regex.IsMatch(p, "[a-z]") || RegraUsuario.SENHA_REQUER_MINUSCULO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerMinusculo)
                .Must(p => Regex.IsMatch(p, "[0-9]") || RegraUsuario.SENHA_REQUER_DIGITO == false)
                    .WithMessage(_resource.UsuarioSenhaRequerDigito)
                .Must(p => Regex.IsMatch(p, "[^a-zA-Z0-9]") || RegraUsuario.SENHA_REQUER_ESPECIAL == false)
                    .WithMessage(_resource.UsuarioSenhaRequerEspecial);
        }

        private void ValidarConfirmacaoSenha()
        {
            RuleFor(p => p.ConfirmacaoSenha)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioConfirmacaoSenhaObrigatoria)
                .Equal(p => p.Senha)
                    .WithMessage(_resource.UsuarioConfirmacaoSenhaNaoConfere);
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty()
                    .WithMessage(_resource.UsuarioImagemObrigatoria);
        }

        private void ValidarPessoaId()
        {
            RuleFor(p => p.PessoaId)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio());
        }



    }
}
