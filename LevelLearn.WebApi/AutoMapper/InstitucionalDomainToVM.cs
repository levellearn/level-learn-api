using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Institucional.Turma;
using LevelLearn.ViewModel.Pessoas;

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
            InstituicaoMap();
            CursoMap();
            TurmaMap();          
        }

        private void InstituicaoMap()
        {
            CreateMap<Instituicao, InstituicaoDetalheVM>()
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
        }

        private void CursoMap()
        {
            CreateMap<Curso, CursoVM>();
            CreateMap<Curso, CursoDetalheVM>();
        }

        private void TurmaMap()
        {
            CreateMap<Turma, TurmaVM>();
            CreateMap<Turma, TurmaDetalheVM>();
        }


    }
}
