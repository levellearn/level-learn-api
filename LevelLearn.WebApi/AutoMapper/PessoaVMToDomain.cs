using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.ViewModel.Pessoas;
using LevelLearn.ViewModel.Usuarios;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Pessoa VM To Domain
    /// </summary>
    public class PessoaVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public PessoaVMToDomain()
        {
            PessoaMap();
            UsuarioMap();
        }

        private void PessoaMap()
        {
            // Herança
            CreateMap<RegistrarPessoaVM, Pessoa>()
               .Include<RegistrarProfessorVM, Professor>()
               .Include<RegistrarAlunoVM, Aluno>();

            CreateMap<RegistrarProfessorVM, Professor>();
            CreateMap<RegistrarAlunoVM, Aluno>();

            // Herança
            //CreateMap<PessoaAtualizaVM, Pessoa>()
               //.Include<ProfessorAtualizaVM, Professor>()
               //.Include<AlunoAtualizaVM, Aluno>();

            CreateMap<AlunoAtualizaVM, Aluno>();
            //CreateMap<ProfessorAtualizaVM, Professor>();

            CreateMap<AlunoVM, Aluno>();
        }

        private void UsuarioMap()
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
