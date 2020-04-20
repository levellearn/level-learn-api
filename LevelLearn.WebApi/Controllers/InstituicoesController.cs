using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class InstituicoesController : ControllerBase
    {
        private readonly IInstituicaoService _instituicaoService;
        private readonly IMapper _mapper;

        public InstituicoesController(IInstituicaoService instituicaoService, IMapper mapper)
        {
            _instituicaoService = instituicaoService;
            _mapper = mapper;
        }

        [Authorize(Roles = ApplicationRoles.ADMIN)]
        [HttpGet("v1/[controller]/admin")]
        [ProducesResponseType(typeof(InstituicaoListVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoesAdmin(
            [FromQuery]string query,
            [FromQuery][Range(1, int.MaxValue)]int pageNumber,
            [FromQuery][Range(1, 200)]int pageSize)
        {
            var instituicoes = await _instituicaoService.GetWithPagination(query, pageNumber, pageSize);
            var count = await _instituicaoService.CountWithPagination(query);

            var listVM = new InstituicaoListVM
            {
                Data = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(instituicoes),
                Total = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(listVM);
        }

        /// <summary>
        /// Retorna todas as instituições de um professor paginadas
        /// </summary>        
        /// <param name="query">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista instituições</returns>
        /// <response code="200">Lista de instituições</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]", Name = "GetInstituicoes")]
        [ProducesResponseType(typeof(InstituicaoListVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoes(
            [FromQuery]string query,
            [FromQuery][Range(1, int.MaxValue)]int pageNumber,
            [FromQuery][Range(1, 200)]int pageSize)
        {
            var queryVM = new PaginationQueryVM(query, pageNumber, pageSize);

            var response = await _instituicaoService.ObterInstituicoesProfessor(User.GetPessoaId(), queryVM);

            var listVM = new InstituicaoListVM
            {
                Data = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(response.Data),
                Total = response.Total.Value,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(listVM);
        }

        /// <summary>
        /// Retorna uma instituição de um professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Instituição</returns>
        /// <response code="200">Retorna uma instituição de um professor</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/{id:guid}")]
        [ProducesResponseType(typeof(InstituicaoVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetInstituicao(Guid id)
        {
            ResponseAPI<Instituicao> response = await _instituicaoService.ObterInstituicao(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return Ok(_mapper.Map<InstituicaoVM>(response.Data));
        }

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

        [HttpPut("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditInstituicao(Guid id, [FromBody] EditarInstituicaoVM instituicaoVM)
        {
            var response = await _instituicaoService.EditarInstituicao(id, instituicaoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        [HttpDelete("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteInstituicao(Guid id)
        {
            var response = await _instituicaoService.RemoverInstituicao(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }


    }
}
