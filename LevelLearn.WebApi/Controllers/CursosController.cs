using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    /// <summary>
    /// Cursos Controller
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cursoService">ICursoService</param>
        /// <param name="mapper">IMapper</param>
        public CursosController(ICursoService cursoService, IMapper mapper)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        ///// <summary>
        ///// Retorna todos os cursos de um professor paginadas com filtro por nome
        ///// </summary>        
        ///// <param name="query">Termo de pesquisa</param>
        ///// <param name="pageNumber">Número da página</param>
        ///// <param name="pageSize">Quantidade de itens por página</param>
        ///// <returns>Lista cursos</returns>
        ///// <response code="200">Lista de cursos</response>
        ///// <response code="500">Ops, ocorreu um erro no sistema!</response>
        //[HttpGet("v1/[controller]", Name = "GetInstituicoes")]
        //[ProducesResponseType(typeof(CursoListVM), StatusCodes.Status200OK)]
        //public async Task<ActionResult> GetInstituicoes(
        //    [FromQuery]string query,
        //    [FromQuery][Range(1, int.MaxValue)]int pageNumber,
        //    [FromQuery][Range(1, 100)]int pageSize)
        //{
        //    var queryVM = new PaginationQueryVM(query, pageNumber, pageSize);

        //    ResponseAPI<IEnumerable<Curso>> response = await _cursoService.ObterInstituicoesProfessor(User.GetPessoaId(), queryVM);

        //    var listVM = new CursoListVM
        //    {
        //        Data = _mapper.Map<IEnumerable<Curso>, IEnumerable<CursoVM>>(response.Data),
        //        Total = response.Total.Value,
        //        PageNumber = response.PageNumber.Value,
        //        PageSize = response.PageSize.Value
        //    };

        //    return Ok(listVM);
        //}

        ///// <summary>
        ///// Retorna um curso
        ///// </summary>
        ///// <param name="id">Id Curso</param>
        ///// <returns>Curso</returns>
        ///// <response code="200">Retorna um curso</response>
        ///// <response code="404">Curso não encontrado</response>
        ///// <response code="500">Ops, ocorreu um erro no sistema!</response>
        //[HttpGet("v1/[controller]/{id:guid}")]
        //[ProducesResponseType(typeof(CursoVM), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> GetCurso(Guid id)
        //{
        //    ResponseAPI<Curso> response = await _cursoService.ObterCurso(id, User.GetPessoaId());

        //    if (response.Failure) return StatusCode(response.StatusCode, response);

        //    return Ok(_mapper.Map<CursoVM>(response.Data));
        //}

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
        public async Task<ActionResult> CreateCurso([FromBody] CadastrarCursoVM cursoVM)
        {
            ResponseAPI<Curso> response = await _cursoService.CadastrarCurso(cursoVM, User.GetPessoaId());

            if (response.Failure) return StatusCode(response.StatusCode, response);

            var responseVM = _mapper.Map<CursoVM>(response.Data);

            return CreatedAtAction("GetCurso", new { id = responseVM.Id }, responseVM);
            //return CreatedAtAction(nameof(GetCurso), new { id = responseVM.Id }, responseVM);
        }

        ///// <summary>
        ///// Edição de curso
        ///// </summary>
        ///// <param name="id">Id curso</param>
        ///// <param name="instituicaoVM">Dados de edição do curso</param>
        ///// <returns></returns>
        ///// <response code="204">Sem Conteúdo</response>
        ///// <response code="400">Dados inválidos</response>
        ///// <response code="403">Não é admin do curso</response>
        ///// <response code="404">Curso não encontrado</response>
        ///// <response code="500">Ops, ocorreu um erro no sistema!</response>
        //[HttpPut("v1/[controller]/{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> EditCurso(Guid id, [FromBody] EditarCursoVM instituicaoVM)
        //{
        //    var response = await _cursoService.EditarCurso(id, instituicaoVM, User.GetPessoaId());

        //    if (response.Failure) return StatusCode(response.StatusCode, response);

        //    return NoContent();
        //}

        ///// <summary>
        ///// Remoção de curso
        ///// </summary>
        ///// <param name="id">Id curso</param>
        ///// <returns></returns>
        ///// <response code="204">Sem Conteúdo</response>
        ///// <response code="403">Não é admin do curso</response>
        ///// <response code="404">Curso não encontrado</response>
        ///// <response code="500">Ops, ocorreu um erro no sistema!</response>
        //[HttpDelete("v1/[controller]/{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> DeleteCurso(Guid id)
        //{
        //    var response = await _cursoService.RemoverCurso(id, User.GetPessoaId());

        //    if (response.Failure) return StatusCode(response.StatusCode, response);

        //    return NoContent();
        //}


    }
}
