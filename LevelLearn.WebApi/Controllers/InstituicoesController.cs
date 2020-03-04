using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.WebApi.ViewModels.Institucional.Instituicao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InstituicoesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public InstituicoesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //[Route("api/v1/[controller]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InstituicaoListVM>> Get()
        {
            try
            {
                var instituicoes = await _uow.Instituicoes.GetAllAsync();
                var count = await _uow.Instituicoes.CountAsync();

                var response = new InstituicaoListVM();
                var listVM = new List<InstituicaoVM>();
                foreach (var item in instituicoes)
                {
                    var vm = new InstituicaoVM { Id = item.Id, Nome = item.Nome, Descricao = item.Descricao };
                    listVM.Add(vm);
                }

                //new InstituicaoListVM { Customers = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(instituicoes), Count = count }
                response.Data = listVM;
                response.Count = count;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno do servidor");
            }
        }

        //[Route("api/v1/[controller]")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(Guid id)
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
