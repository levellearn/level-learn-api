using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.ValueObjects;
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
            CreateMap<RegistrarProfessorVM, Professor>()
               .ConstructUsing(p =>
                   new Professor(p.Nome, new Email(p.Email), new CPF(p.Cpf), new Celular(p.Celular), p.Genero, p.DataNascimento)
               );

            CreateMap<RegistrarAlunoVM, Aluno>()
               .ConstructUsing(p =>
                   new Aluno(p.Nome, new Email(p.Email), new CPF(p.Cpf), new Celular(p.Celular), p.RA, p.Genero, p.DataNascimento)
               );
        }

    }
}
