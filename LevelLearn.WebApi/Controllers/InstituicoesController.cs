using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{

    /// <summary>
    /// Instituicoes Controller
    /// </summary>   
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class InstituicoesController : MyBaseController
    {
        private readonly IInstituicaoService _instituicaoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="instituicaoService">IInstituicaoService</param>
        /// <param name="mapper">IMapper</param>
        public InstituicoesController(IInstituicaoService instituicaoService, IMapper mapper)
            : base(mapper)
        {
            _instituicaoService = instituicaoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as instituições paginadas com filtro nome
        /// </summary>        
        /// <param name="filterVM">Armazena os filtros de consulta</param>
        /// <returns>Lista instituições</returns>
        /// <response code="200">Lista de instituições</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [Authorize(Roles = ApplicationRoles.ADMIN)]
        [HttpGet("v1/[controller]/admin")]
        [ProducesResponseType(typeof(ListaPaginadaVM<InstituicaoVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObterInstituicoesAdmin([FromBody]FiltroPaginacaoVM filterVM)
        {
            var instituicoes = await _instituicaoService.GetWithPagination(filterVM.SearchFilter, filterVM.PageNumber, filterVM.PageSize);
            int count = await _instituicaoService.CountWithPagination(filterVM.SearchFilter);

            var listaVM = _mapper.Map<IEnumerable<InstituicaoVM>>(instituicoes);

            return Ok(CriarListaPaginada(listaVM, count, filterVM));
        }

        /// <summary>
        /// Retorna todas as instituições de um professor paginadas e filtro
        /// </summary>        
        /// <param name="filterVM">Armazena os filtros de consulta</param>
        /// <returns>Lista instituições</returns>
        /// <response code="200">Lista de instituições</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]", Name = "ObterInstituicoes")]
        [ProducesResponseType(typeof(ListaPaginadaVM<InstituicaoVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObterInstituicoes([FromBody]FiltroPaginacaoVM filterVM)
        {
            var filtroPaginacao = _mapper.Map<FiltroPaginacao>(filterVM);

            ResponseAPI<IEnumerable<Instituicao>> response =
                await _instituicaoService.ObterInstituicoesProfessor(User.GetPessoaId(), filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(response.Data);

            return Ok(CriarListaPaginada(listaVM, response.Total.Value, filterVM));
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
        public async Task<ActionResult> ObterInstituicao(Guid id)
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
        public async Task<ActionResult> CriarInstituicao([FromBody] CadastrarInstituicaoVM instituicaoVM)
        {
            ResponseAPI<Instituicao> response = await _instituicaoService.CadastrarInstituicao(instituicaoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            var responseVM = _mapper.Map<InstituicaoVM>(response.Data);

            return CreatedAtAction(nameof(ObterInstituicao), new { id = responseVM.Id }, responseVM);
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
        public async Task<ActionResult> EditarInstituicao(Guid id, [FromBody] EditarInstituicaoVM instituicaoVM)
        {
            var response = await _instituicaoService.EditarInstituicao(id, instituicaoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        /// <summary>
        /// Alternar ativação da instituição
        /// </summary>
        /// <param name="id">Id instituição</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="403">Não é admin da instituição</response>
        /// <response code="404">Instituição não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPatch("v1/[controller]/{id:guid}/alternar-ativacao")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AlternarAtivacaoInstituicao(Guid id)
        {
            var response = await _instituicaoService.AlternarAtivacao(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }


    }
}
