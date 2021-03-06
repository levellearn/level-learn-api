﻿using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.ValueObjects;
using LevelLearn.ViewModel.Pessoas;
using LevelLearn.ViewModel.Usuarios;

namespace LevelLearn.ViewModel.AutoMapper
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
            CreateMap<AlunoVM, Aluno>();
            CreateMap<ProfessorVM, Professor>();

            // Herança
            CreateMap<RegistrarPessoaVM, Pessoa>()
                .Include<RegistrarProfessorVM, Professor>()
                .Include<RegistrarAlunoVM, Aluno>();

            CreateMap<RegistrarProfessorVM, Professor>();
            CreateMap<RegistrarAlunoVM, Aluno>();

            // Herança
            CreateMap<PessoaAtualizaVM, Pessoa>()
                .ForMember(
                    dest => dest.Cpf,
                    opt => opt.MapFrom(src => new CPF(src.Cpf))
                )
                .ForMember(
                    dest => dest.Celular,
                    opt => opt.MapFrom(src => new Celular(src.Celular))
                )
               .Include<ProfessorAtualizaVM, Professor>()
               .Include<AlunoAtualizaVM, Aluno>();

            CreateMap<AlunoAtualizaVM, Aluno>();
            CreateMap<ProfessorAtualizaVM, Professor>();

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
