using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.WebApi.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{

    /// <summary>
    /// Instituicoes Controller
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class InstituicoesController : ControllerBase
    {
        private readonly IInstituicaoService _instituicaoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="instituicaoService">IInstituicaoService</param>
        /// <param name="mapper">IMapper</param>
        public InstituicoesController(IInstituicaoService instituicaoService, IMapper mapper)
        {
            _instituicaoService = instituicaoService;
            _mapper = mapper;
            var resource = new InstituicaoResource();
            string msg = resource.InstituicaoNaoEncontrada;
        }

        /// <summary>
        /// Retorna todas as instituições paginadas com filtro nome
        /// </summary>        
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista instituições</returns>
        /// <response code="200">Lista de instituições</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [Authorize(Roles = ApplicationRoles.ADMIN)]
        [HttpGet("v1/[controller]/admin")]
        [ProducesResponseType(typeof(InstituicaoListaVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoesAdmin(
            [FromQuery]string searchFilter,
            [FromQuery][Range(1, int.MaxValue)]int pageNumber,
            [FromQuery][Range(1, 200)]int pageSize)
        {
            var instituicoes = await _instituicaoService.GetWithPagination(searchFilter, pageNumber, pageSize);
            var count = await _instituicaoService.CountWithPagination(searchFilter);

            var listVM = new InstituicaoListaVM
            {
                Data = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(instituicoes),
                Total = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(listVM);
        }

        /// <summary>
        /// Retorna todas as instituições de um professor paginadas com filtro por nome
        /// </summary>        
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista instituições</returns>
        /// <response code="200">Lista de instituições</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]", Name = "GetInstituicoes")]
        [ProducesResponseType(typeof(InstituicaoListaVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoes(
            [FromQuery]string searchFilter,
            [FromQuery][Range(1, int.MaxValue)]int pageNumber,
            [FromQuery][Range(1, 100)]int pageSize)
        {
            var filterVM = new PaginationFilterVM(searchFilter, pageNumber, pageSize);

            ResponseAPI<IEnumerable<Instituicao>> response =
                await _instituicaoService.ObterInstituicoesProfessor(User.GetPessoaId(), filterVM);

            var listVM = new InstituicaoListaVM
            {
                Data = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(response.Data),
                Total = response.Total.Value,
                PageNumber = response.PageNumber.Value,
                PageSize = response.PageSize.Value
            };

            return Ok(listVM);
        }

        /// <summary>
        /// Retorna uma instituição
        /// </summary>
        /// <param name="id">Id Instituição</param>
        /// <returns>Instituição</returns>
        /// <response code="200">Retorna uma instituição</response>
        /// <response code="404">Instituição não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/{id:guid}")]
        [ProducesResponseType(typeof(InstituicaoDetalheVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetInstituicao(Guid id)
        {
            ResponseAPI<Instituicao> response = await _instituicaoService.ObterInstituicao(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return Ok(_mapper.Map<InstituicaoDetalheVM>(response.Data));
        }

        /// <summary>
        /// Cadastro de instituição
        /// </summary>
        /// <param name="instituicaoVM">Dados de cadastro da instituição</param>
        /// <returns>Retorna a instituição cadastrada</returns>
        /// <response code="201">Retorna instituição cadastrada</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]")]
        [ProducesResponseType(typeof(InstituicaoVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateInstituicao([FromBody] CadastrarInstituicaoVM instituicaoVM)
        {
            ResponseAPI<Instituicao> response = await _instituicaoService.CadastrarInstituicao(instituicaoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            var responseVM = _mapper.Map<InstituicaoVM>(response.Data);

            return CreatedAtAction(nameof(GetInstituicao), new { id = responseVM.Id }, responseVM);
        }

        /// <summary>
        /// Edição de instituição
        /// </summary>
        /// <param name="id">Id instituição</param>
        /// <param name="instituicaoVM">Dados de edição da instituição</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="403">Não é admin da instituição</response>
        /// <response code="404">Instituição não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPut("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditInstituicao(Guid id, [FromBody] EditarInstituicaoVM instituicaoVM)
        {
            var response = await _instituicaoService.EditarInstituicao(id, instituicaoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        /// <summary>
        /// Remoção de instituição
        /// </summary>
        /// <param name="id">Id instituição</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="403">Não é admin da instituição</response>
        /// <response code="404">Instituição não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpDelete("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteInstituicao(Guid id)
        {
            var response = await _instituicaoService.RemoverInstituicao(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }


    }
}
