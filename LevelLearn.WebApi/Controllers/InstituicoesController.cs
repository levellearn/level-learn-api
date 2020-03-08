using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.WebApi.ViewModels.Institucional.Instituicao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class InstituicoesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InstituicoesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Route("v1/[controller]")]
        [HttpGet]
        [ProducesResponseType(typeof(InstituicaoListVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoes([FromQuery]string query, [FromQuery]int pageIndex, [FromQuery]int pageSize)
        {
            try
            {
                var instituicoes = await _uow.Instituicoes.GetWithPagination(query, pageIndex, pageSize);
                var count = await _uow.Instituicoes.CountWithPagination(query);

                var response = new InstituicaoListVM
                {
                    Instituicoes = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(instituicoes),
                    Count = count,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Query = query
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno do servidor");
            }
        }

        [Route("v1/[controller]/{id:guid}")]
        [HttpGet]
        [ProducesResponseType(typeof(InstituicaoVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetInstituicao(Guid id)
        {
            try
            {
                var instituicao = await _uow.Instituicoes.GetAsync(id);

                if (instituicao == null) return NotFound("Instituição não encontrada");

                return Ok(_mapper.Map<InstituicaoVM>(instituicao));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno do servidor");
            }
        }

        [Route("v1/[controller]")]
        [HttpPost]
        public void CreateInstituicao([FromBody] string value)
        {
        }

        [Route("v1/[controller]/{id:guid}")]
        public void EditInstituicao(Guid id, [FromBody] string value)
        {
        }

        [Route("v1/[controller]/{id:guid}")]
        public void DeleteInstituicao(Guid id)
        {
        }


    }
}
