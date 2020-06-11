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
            ComumMap();
        }

        private void ComumMap()
        {
            CreateMap<FiltroPaginacaoVM, FiltroPaginacao>()
                .ConstructUsing(p =>
                    new FiltroPaginacao(p.FiltroPesquisa, p.NumeroPagina, p.TamanhoPorPagina, p.OrdenarPor, p.OrdenacaoAscendente, p.Ativo)
                );
        }

    }

}
