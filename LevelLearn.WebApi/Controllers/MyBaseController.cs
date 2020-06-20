using AutoMapper;
using LevelLearn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace LevelLearn.WebApi.Controllers
{

    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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
        /// Verificar API
        /// </summary>
        /// <returns>It Works</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("up")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Up()
        {
            return Ok("API funcionando");
        }

        /// <summary>
        /// Cria a lista paginada
        /// </summary>
        /// <typeparam name="T">Tipo da lista</typeparam>
        /// <param name="listaVM">Lista View Model</param>
        /// <param name="total">Total da entidade no banco</param>
        /// <param name="filterVM">Filtro de pesquisa</param>
        /// <returns></returns>
        protected ListaPaginadaVM<T> CriarListaPaginada<T>(IEnumerable<T> listaVM, int? total, FiltroPaginacaoVM filterVM)
            where T : class
        {
            var listaPaginadaVM = new ListaPaginadaVM<T>(listaVM, total, filterVM);

            return listaPaginadaVM;
        }

    }

}