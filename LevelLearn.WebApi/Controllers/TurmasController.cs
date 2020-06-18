﻿using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    /// <summary>
    /// Turmas Controller
    /// </summary>    
    [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
    public class TurmasController : MyBaseController
    {
        private readonly ITurmaService _turmaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="turmaService">ITurmaService</param>
        /// <param name="mapper">IMapper</param>
        public TurmasController(ITurmaService turmaService, IMapper mapper) : base(mapper)
        {
            _turmaService = turmaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as turmas de um curso de um professor paginadas com filtro
        /// </summary>        
        /// <param name="cursoId">Id curso</param>
        /// <param name="filtroVM">Armazena os filtros de consulta</param>
        /// <returns>Lista turmas</returns>
        /// <response code="200">Lista de turmas</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/curso/{cursoId:guid}")]
        [ProducesResponseType(typeof(ListaPaginadaVM<TurmaVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObterTurmas([FromRoute] Guid cursoId, [FromBody] FiltroPaginacaoVM filtroVM)
        {
            var filtroPaginacao = _mapper.Map<FiltroPaginacao>(filtroVM);

            ResultadoService<IEnumerable<Turma>> resultado =
                await _turmaService.TurmasCursoProfessor(cursoId, User.GetPessoaId(), filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<TurmaVM>>(resultado.Dados);

            return Ok(CriarListaPaginada(listaVM, resultado.Total.Value, filtroVM));
        }

        /// <summary>
        /// Retorna um turma
        /// </summary>
        /// <param name="id">Id Turma</param>
        /// <returns>Turma</returns>
        /// <response code="200">Retorna um turma</response>
        /// <response code="404">Turma não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/{id:guid}")]
        [ProducesResponseType(typeof(TurmaDetalheVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObterTurma(Guid id)
        {
            ResultadoService<Turma> resultado = await _turmaService.ObterTurma(id, User.GetPessoaId());

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return Ok(_mapper.Map<TurmaDetalheVM>(resultado.Dados));
        }

        /// <summary>
        /// Cadastro de turma
        /// </summary>
        /// <param name="turmaVM">Dados de cadastro da turma</param>
        /// <returns>Retorna a turma cadastrada</returns>
        /// <response code="201">Retorna turma cadastrada</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]")]
        [ProducesResponseType(typeof(TurmaVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CriarTurma([FromBody] CadastrarTurmaVM turmaVM)
        {
            Guid pessoaId = User.GetPessoaId();

            if (turmaVM.ProfessorId == Guid.Empty || turmaVM.ProfessorId != pessoaId)
                turmaVM.ProfessorId = pessoaId;

            var turma = _mapper.Map<Turma>(turmaVM);

            ResultadoService<Turma> resultado = await _turmaService.CadastrarTurma(turma, pessoaId);

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            TurmaVM respostaVM = _mapper.Map<TurmaVM>(resultado.Dados);

            return CreatedAtAction(nameof(ObterTurma), new { id = respostaVM.Id }, respostaVM);
        }

        /// <summary>
        /// Edição de turma
        /// </summary>
        /// <param name="id">Id turma</param>
        /// <param name="turmaVM">Dados de edição da turma</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="403">Não é admin da turma</response>
        /// <response code="404">Turma não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPut("v1/[controller]/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditarTurma(Guid id, [FromBody] EditarTurmaVM turmaVM)
        {
            var resultado = await _turmaService.EditarTurma(id, turmaVM, User.GetPessoaId());

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return NoContent();
        }

        /// <summary>
        /// Alternar ativação da turma
        /// </summary>
        /// <param name="id">Id turma</param>
        /// <returns></returns>
        /// <response code="204">Sem Conteúdo</response>
        /// <response code="403">Não é admin da turma</response>
        /// <response code="404">Turma não encontrada</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpDelete("v1/[controller]/{id:guid}/alternar-ativacao")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AlternarAtivacaoTurma(Guid id)
        {
            var resultado = await _turmaService.AlternarAtivacaoTurma(id, User.GetPessoaId());

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return NoContent();
        }


    }
}
