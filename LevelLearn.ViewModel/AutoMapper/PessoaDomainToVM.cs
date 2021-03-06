﻿using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.ViewModel.Pessoas;
using LevelLearn.ViewModel.Usuarios;

namespace LevelLearn.ViewModel.AutoMapper
{
    /// <summary>
    /// Pessoa Domain To VM
    /// </summary>
    public class PessoaDomainToVM : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public PessoaDomainToVM()
        {
            PessoaMap();          

            CreateMap<Usuario, UsuarioVM>();

            CreateMap<PessoaInstituicao, PessoaInstituicaoVM>();
            CreateMap<PessoaCurso, PessoaCursoVM>();
        }

        private void PessoaMap()
        {
            CreateMap<Professor, ProfessorVM>();
            CreateMap<Professor, ProfessorAtualizaVM>();
            CreateMap<Aluno, AlunoVM>();
            CreateMap<Aluno, AlunoAtualizaVM>();

            CreateMap<Pessoa, PessoaVM>()
                .ForMember(
                    dest => dest.Genero,
                    opt => opt.MapFrom(src => src.Genero.GetDescriptionLocalized())
                )
                .ForMember(
                    dest => dest.TipoPessoa,
                    opt => opt.MapFrom(src => src.TipoPessoa.GetDescriptionLocalized())
                );

            CreateMap<Pessoa, PessoaDetalheVM>()
                .ForMember(
                    dest => dest.Genero,
                    opt => opt.MapFrom(src => src.Genero.GetDescriptionLocalized())
                )
                .ForMember(
                    dest => dest.TipoPessoa,
                    opt => opt.MapFrom(src => src.TipoPessoa.GetDescriptionLocalized())
                );
        }

    }
}
