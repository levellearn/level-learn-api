using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Pessoas;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.WebApi.Controllers
{
    /// <summary>
    /// Professores Controller
    /// </summary>
    public class ProfessoresController : MyBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IProfessorService _professorService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="usuarioService"></param>
        /// <param name="professorService"></param>
        public ProfessoresController(IMapper mapper, IUsuarioService usuarioService, IProfessorService professorService)
            : base(mapper)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _professorService = professorService;
        }

        /// <summary>
        /// Registro de usuário professor
        /// </summary>
        /// <param name="registrarProfessorVM">Dados de cadastro do usuário professor</param>
        /// <returns>Sem conteúdo</returns>     
        [HttpPost("v1/[controller]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultadoService), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarProfessor(RegistrarProfessorVM registrarProfessorVM)
        {
            var professor = _mapper.Map<Professor>(registrarProfessorVM);
            var usuario = _mapper.Map<Usuario>(registrarProfessorVM);

            ResultadoService<Usuario> resultado = await _usuarioService.RegistrarProfessor(professor, usuario);

            if (!resultado.Sucesso) return StatusCode(resultado.StatusCode, resultado);

            return StatusCode(resultado.StatusCode, _mapper.Map<UsuarioVM>(resultado.Dados));
        }

        /// <summary>
        ///  Retorna todos os professores de uma instituicao - paginado com filtro
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtroVM"></param>
        /// <returns>Lista de alunos</returns>        
        [Authorize(Roles = ApplicationRoles.ADMIN_E_PROFESSOR)]
        [HttpGet("v1/[controller]/instituicao/{instituicaoId:guid}")]
        [ProducesResponseType(typeof(ListaPaginadaVM<ProfessorVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObterAlunosPorInstituicao([FromRoute] Guid instituicaoId, [FromBody] FiltroPaginacaoVM filtroVM)
        {
            var filtroPaginacao = _mapper.Map<FiltroPaginacao>(filtroVM);

            ResultadoService<IEnumerable<Professor>> resultado =
                await _professorService.ObterProfessorsPorInstituicao(instituicaoId, filtroPaginacao);

            var listaVM = _mapper.Map<IEnumerable<ProfessorVM>>(resultado.Dados);

            return Ok(CriarListaPaginada(listaVM, resultado.Total, filtroVM));
        }

        /// <summary>
        /// Atualiza propriedades do profesor
        /// </summary>
        /// <remarks>
        /// Exemplo
        ///     [{ "op": "replace", "path": "/ra", "value": "123456" }]
        /// </remarks>
        /// <param name="id">Id professor</param>
        /// <param name="patchProfessor"></param>
        /// <returns></returns>
        [Authorize(Roles = ApplicationRoles.ADMIN_E_PROFESSOR)]
        [HttpPatch("v1/[controller]/{id:guid}")]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(typeof(ProfessorAtualizaVM), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResultadoService), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoService), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResultadoService), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar([FromRoute] Guid id, [FromBody] JsonPatchDocument<ProfessorAtualizaVM> patchProfessor)
        {
            if (id == Guid.Empty || id != User.GetPessoaId()) return Forbid();

            if (patchProfessor == null) return BadRequest(ModelState);

            Professor profesorDb = await _professorService.GetAsync(id);
            if (profesorDb == null) return NotFound();

            var professorVMToPatch = _mapper.Map<ProfessorAtualizaVM>(profesorDb);
            patchProfessor.ApplyTo(professorVMToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            profesorDb = _mapper.Map<Professor>(professorVMToPatch);

            ResultadoService resultado = await _professorService.Atualizar(profesorDb);

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return NoContent();
        }


    }
}
