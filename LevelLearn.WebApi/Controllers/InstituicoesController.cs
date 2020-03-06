using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.WebApi.ViewModels.Institucional.Instituicao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InstituicaoListVM>>GetInstituicoes([FromQuery]string query, [FromQuery]int pageIndex, [FromQuery]int pageSize)
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

        //[Route("api/v1/[controller]")]
        [HttpGet("{id}", Name = "GetInstituicao")]
        public string GetInstituicao(Guid id)
        {
            return "value";
        }

        // POST: api/Instituicoes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Instituicoes/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
        }

        // DELETE: api/Instituicoes/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }


    }
}
