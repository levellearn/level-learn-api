using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.ViewModel.Enums;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Pessoas;
using System;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Institucional Domain To VM
    /// </summary>
    public class InstitucionalDomainToVM : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InstitucionalDomainToVM()
        {
            // TODO: EnumExtension.GetDescriptionLocalized
            CreateMap<Instituicao, InstituicaoDetalheVM>();

            CreateMap<Instituicao, InstituicaoVM>()
                .ForMember(
                    dest => dest.NivelEnsino,
                    opt => opt.MapFrom(src => EnumExtension.GetDescriptionLocalized(src.NivelEnsino))
                )
                .ForMember(
                    dest => dest.OrganizacaoAcademica,
                    opt => opt.MapFrom(src => EnumExtension.GetDescriptionLocalized(src.OrganizacaoAcademica))
                )
                .ForMember(
                    dest => dest.Rede,
                    opt => opt.MapFrom(src => EnumExtension.GetDescriptionLocalized(src.Rede))
                )
                .ForMember(
                    dest => dest.CategoriaAdministrativa,
                    opt => opt.MapFrom(src => EnumExtension.GetDescriptionLocalized(src.CategoriaAdministrativa))
                );


            CreateMap<Curso, CursoVM>();
            CreateMap<Curso, CursoDetalheVM>();

            CreateMap<Pessoa, PessoaVM>();

            CreateMap<PerfilInstituicao, PerfilInstituicaoVM>();
            CreateMap<GeneroPessoa, GeneroPessoaVM>();
            CreateMap<TipoPessoa, TipoPessoaVM>();
            CreateMap<OrganizacaoAcademica, OrganizacaoAcademicaVM>();
            CreateMap<Rede, RedeIntituicaoVM>();
            CreateMap<CategoriaAdministrativa, CategoriaAdministrativaVM>();
            CreateMap<NivelEnsino, NivelEnsinoVM>();

            CreateMap<PessoaInstituicao, PessoaInstituicaoVM>();
            CreateMap<PessoaCurso, PessoaCursoVM>();

        }
    }
}
