using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.ViewModel.Institucional.Instituicao;

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
            CreateMap<Instituicao, InstituicaoVM>();
            CreateMap<Curso, CursoVM>();

        }
    }
}
