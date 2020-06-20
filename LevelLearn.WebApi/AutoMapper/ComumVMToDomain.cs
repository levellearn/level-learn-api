using AutoMapper;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.ViewModel;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Comum VM To Domain
    /// </summary>
    public class ComumVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ComumVMToDomain()
        {
            CreateMap<FiltroPaginacaoVM, FiltroPaginacao>();
        }       

    }

}
