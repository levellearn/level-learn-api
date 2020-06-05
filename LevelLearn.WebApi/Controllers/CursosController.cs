using AutoMapper;
using LevelLearn.Domain.Entities.Comum;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Curso;
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
    /// Cursos Controller
    /// </summary>    
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class CursosController : MyBaseController
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cursoService">ICursoService</param>
        /// <param name="mapper">IMapper</param>
        public CursosController(ICursoService cursoService, IMapper mapper) : base(mapper)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os cursos de uma instituição de um professor paginadas com filtro
        /// </summary>        
        /// <param name="instituicaoId">Id instituição</param>
        /// <param name="filterVM">Armazena os filtros de consulta</param>
        /// <returns>Lista cursos</returns>
        /// <response code="200">Lista de cursos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/intituicao/{instituicaoId:guid}")]
        [ProducesResponseType(typeof(PaginatedListVM<CursoVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObterCursos([FromRoute]Guid instituicaoId, [FromBody]PaginationFilterVM filterVM)
        {
            var filtroPaginacao = _mapper.Map<PaginationFilterVM, FiltroPaginacao>(filterVM);

            ResponseAPI<IEnumerable<Curso>> response =
                await _cursoService.CursosInstituicaoProfessor(instituicaoId, User.GetPessoaId(), filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<Curso>, IEnumerable<CursoVM>>(response.Data);

            return Ok(CriarListaPaginada(listaVM, response.Total.Value, filterVM));
        }

        /// <summary>
        /// Retorna um curso
        /// </summary>
        /// <param name="id">Id Curso</param>
        /// <returns>Curso</returns>
        /// <response code="200">Retorna um curso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/{id:guid}")]
        [ProducesResponseType(typeof(CursoDetalheVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObterCurso(Guid id)
        {
            ResponseAPI<Curso> response = await _cursoService.ObterCurso(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return Ok(_mapper.Map<CursoDetalheVM>(response.Data));
        }

        /// <summary>
        /// Cadastro de curso
        /// </summary>
        /// <param name="cursoVM">Dados de cadastro do curso</param>
        /// <returns>Retorna o curso cadastrado</returns>
        /// <response code="201">Retorna curso cadastrado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]")]
        [ProducesResponseType(typeof(CursoVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CriarCurso([FromBody] CadastrarCursoVM cursoVM)
        {
            ResponseAPI<Curso> response = await _cursoService.CadastrarCurso(cursoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            var responseVM = _mapper.Map<CursoVM>(response.Data);

            return CreatedAtAction(nameof(ObterCurso), new { id = responseVM.Id }, responseVM);
        }

        /// <summary>
        /// Edição de curso
        /// </summary>
        /// <param name="id">Id curso</param>
        /// <param name="cursoVM">Dados de edição do curso</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="403">Não é admin do curso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPut("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditarCurso(Guid id, [FromBody] EditarCursoVM cursoVM)
        {
            var response = await _cursoService.EditarCurso(id, cursoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        /// <summary>
        /// Alternar ativação do curso
        /// </summary>
        /// <param name="id">Id curso</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="403">Não é admin do curso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpDelete("v1/[controller]/{id:guid}/alternar-ativacao")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AlternarAtivacaoCurso(Guid id)
        {
            var response = await _cursoService.AlternarAtivacaoCurso(id, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return NoContent();
        }


    }
}
