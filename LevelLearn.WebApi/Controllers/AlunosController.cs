﻿using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Pessoas;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LevelLearn.WebApi.Controllers
{
    /// <summary>
    /// AlunosController
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class AlunosController : MyBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IAlunoService _alunoService;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="usuarioService"></param>
        /// <param name="alunoService"></param>
        public AlunosController(IMapper mapper, IUsuarioService usuarioService, IAlunoService alunoService)
            : base(mapper)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Registro de usuário aluno
        /// </summary>
        /// <param name="registrarAlunoVM">Dados de cadastro do usuário aluno</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarAluno(RegistrarAlunoVM registrarAlunoVM)
        {
            var aluno = _mapper.Map<Aluno>(registrarAlunoVM);
            var usuario = _mapper.Map<Usuario>(registrarAlunoVM);

            ResultadoService<Usuario> resultado = await _usuarioService.RegistrarAluno(aluno, usuario);

            if (!resultado.Sucesso) return StatusCode(resultado.StatusCode, resultado);

            return StatusCode(resultado.StatusCode, _mapper.Map<UsuarioVM>(resultado.Dados));
        }


        /// <summary>
        ///  Retorna todos os alunos de um curso - paginado com filtro
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="filtroVM"></param>
        /// <returns>Lista de alunos</returns>        
        /// <response code="200">Lista de alunos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/curso/{cursoId:guid}")]
        [ProducesResponseType(typeof(ListaPaginadaVM<AlunoVM>), StatusCodes.Status200OK)]
        [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
        public async Task<ActionResult> ObterAlunosPorCurso([FromRoute] Guid cursoId, [FromBody] FiltroPaginacaoVM filtroVM)
        {
            var filtroPaginacao = _mapper.Map<FiltroPaginacao>(filtroVM);

            ResultadoService<IEnumerable<Aluno>> resultado =
                await _alunoService.ObterAlunosPorCurso(cursoId, filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<AlunoVM>>(resultado.Dados);

            return Ok(CriarListaPaginada(listaVM, resultado.Total, filtroVM));
        }

        /// <summary>
        ///  Retorna todos os alunos de uma instituicao - paginado com filtro
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtroVM"></param>
        /// <returns>Lista de alunos</returns>        
        /// <response code="200">Lista de alunos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/instituicao/{instituicaoId:guid}")]
        [ProducesResponseType(typeof(ListaPaginadaVM<AlunoVM>), StatusCodes.Status200OK)]
        [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.PROFESSOR)]
        public async Task<ActionResult> ObterAlunosPorInstituicao([FromRoute] Guid instituicaoId, [FromBody] FiltroPaginacaoVM filtroVM)
        {
            var filtroPaginacao = _mapper.Map<FiltroPaginacao>(filtroVM);

            ResultadoService<IEnumerable<Aluno>> resultado =
                await _alunoService.ObterAlunosPorInstituicao(instituicaoId, filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<AlunoVM>>(resultado.Dados);

            return Ok(CriarListaPaginada(listaVM, resultado.Total, filtroVM));
        }

        /// <summary>
        /// Atualiza propriedade do aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchAluno"></param>
        /// <returns></returns>
        [HttpPatch("v1/[controller]/{id:guid}")]
        [Authorize(Roles = ApplicationRoles.ADMIN + "," + ApplicationRoles.ALUNO)]
        [ProducesResponseType(typeof(AlunoVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<AlunoVM> patchAluno)
        {
            Aluno alunoDb = await _alunoService.GetAsync(id);
            var alunoVM = _mapper.Map<AlunoVM>(alunoDb);
            patchAluno.ApplyTo(alunoVM);
            alunoDb = _mapper.Map<Aluno>(alunoVM);

            ResultadoService<Aluno> resultado = await _alunoService.Atualuzar(alunoDb);

            if (!resultado.Sucesso) 
                return StatusCode(resultado.StatusCode, resultado);

            return StatusCode(resultado.StatusCode, _mapper.Map<AlunoVM>(resultado.Dados));
        }

    }
}