using AutoMapper;
using LevelLearn.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LevelLearn.WebApi.Controllers
{

    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public abstract class MyBaseController : ControllerBase
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mapper">IMapper</param>
        public MyBaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Cria a lista paginada
        /// </summary>
        /// <typeparam name="T">Tipo da lista</typeparam>
        /// <param name="listaVM">Lista View Model</param>
        /// <param name="total">Total da entidade no banco</param>
        /// <param name="filterVM">Filtro de pesquisa</param>
        /// <returns></returns>
        protected PaginatedListVM<T> CriarListaPaginada<T>(
            IEnumerable<T> listaVM, int total, PaginationFilterVM filterVM) where T : class
        {
            return new PaginatedListVM<T>(
                listaVM, filterVM.PageNumber, filterVM.PageSize, total,
                filterVM.SearchFilter, filterVM.SortBy, filterVM.AscendingSort, filterVM.IsActive);
        }

    }

}