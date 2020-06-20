using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.ViewModel.Usuarios;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Usuário VM To Domain
    /// </summary>
    public class UsuarioVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public UsuarioVMToDomain()
        {
            CreateMap<RegistrarProfessorVM, Usuario>()
                .ConstructUsing(p =>
                    new Usuario(p.Nome, p.NickName, p.Email, p.Celular, p.Senha, p.ConfirmacaoSenha)
                );

            CreateMap<RegistrarAlunoVM, Usuario>()
               .ConstructUsing(p =>
                   new Usuario(p.Nome, p.NickName, p.Email, p.Celular, p.Senha, p.ConfirmacaoSenha)
               );
        }

    }

}
