﻿using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Institucional;
using LevelLearn.ViewModel.Institucional.Instituicao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class InstituicoesController : ControllerBase
    {
        private readonly IInstituicaoService _instituicaoService;
        private readonly IMapper _mapper;

        public InstituicoesController(IInstituicaoService instituicaoService, IMapper mapper)
        {
            _instituicaoService = instituicaoService;
            _mapper = mapper;
        }

        [Route("v1/[controller]")]
        [HttpGet]
        [ProducesResponseType(typeof(InstituicaoListVM), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetInstituicoes(
            [FromQuery]string query,
            [FromQuery][Range(1, int.MaxValue)]int pageIndex,
            [FromQuery][Range(1, 200)]int pageSize)
        {
            try
            {
                var instituicoes = await _instituicaoService.GetWithPagination(query, pageIndex, pageSize);
                var count = await _instituicaoService.CountWithPagination(query);

                var listVM = new InstituicaoListVM
                {
                    Data = _mapper.Map<IEnumerable<Instituicao>, IEnumerable<InstituicaoVM>>(instituicoes),
                    Total = count,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

                return Ok(listVM);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Erro interno do servidor" });
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
                var instituicao = await _instituicaoService.GetAsync(id);

                if (instituicao == null) return NotFound(new { message = "Instituição não encontrada" });

                return Ok(_mapper.Map<InstituicaoVM>(instituicao));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Erro interno do servidor" });
            }
        }

        [Route("v1/[controller]")]
        [HttpPost]
        [ProducesResponseType(typeof(InstituicaoVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateInstituicao([FromBody] CadastrarInstituicaoVM instituicaoVM)
        {
            try
            {
                ResponseAPI response = await _instituicaoService.CadastrarInstituicao(instituicaoVM);

                if (!response.Success) return StatusCode(response.StatusCode, response);

                var responseVM = _mapper.Map<InstituicaoVM>((Instituicao)response.Data);
                return CreatedAtAction(nameof(GetInstituicao), new { id = responseVM.Id }, responseVM);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Erro interno do servidor" });
            }
        }

        [Route("v1/[controller]/{id:guid}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditInstituicao(Guid id, [FromBody] EditarInstituicaoVM instituicaoVM)
        {
            try
            {
                ResponseAPI response = await _instituicaoService.EditarInstituicao(id, instituicaoVM);

                if (!response.Success) return StatusCode(response.StatusCode, response);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Erro interno do servidor" });
            }
        }

        [Route("v1/[controller]/{id:guid}")]
        public void DeleteInstituicao(Guid id)
        {
        }


    }
}
