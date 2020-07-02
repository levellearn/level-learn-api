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

        private void ValidarConfirmacaoSenha()
        {
            RuleFor(p => p.ConfirmacaoSenha)
                .Equal(p => p.Senha).WithMessage(_resource.UsuarioConfirmacaoSenhaNaoConfere);
        }

        private void ValidarImagem()
        {
            RuleFor(p => p.ImagemUrl)
                .NotEmpty().WithMessage(_resource.UsuarioImagemObrigatoria);
        }

        private void ValidarPessoaId()
        {
            RuleFor(p => p.PessoaId)
                .NotEmpty().WithMessage(_resource.IdObrigatorio());
        }



    }
}
