﻿using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    /// <summary>
    /// UsuariosController
    /// </summary>
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="usuarioService">IUsuarioService</param>
        /// <param name="logger">ILogger</param>
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Registro de usuário
        /// </summary>
        /// <param name="usuarioVM">Dados de cadastro do usuário</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/registrar-usuario")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            ResponseAPI<UsuarioVM> response = await _usuarioService.RegistrarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        /// <summary>
        /// Login de usuário
        /// </summary>
        /// <param name="usuarioVM">Dados de login do usuário</param>
        /// <returns>Retorna usuário logado</returns>
        /// <response code="200">Retorna usuário logado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/entrar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioTokenVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginUsuarioVM usuarioVM)
        {
            ResponseAPI<UsuarioTokenVM> response = await _usuarioService.LogarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return Ok(response.Data);
        }

        /// <summary>
        /// Logout de usuário
        /// </summary>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/sair")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout()
        {
            var response = await _usuarioService.Logout(User.GetJWTokenId());

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return NoContent();
        }

        /// <summary>
        /// Verifica a confirmação do email
        /// </summary>
        /// <param name="userId">Id usuário</param>
        /// <param name="confirmationToken">Token de confirmação do email</param>
        /// <returns>Retorna usuário logado</returns>
        /// <response code="200">Retorna usuário logado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">usuário não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/confirmar-email")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioTokenVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ConfirmarEmail([FromQuery]string userId, [FromQuery]string confirmationToken)
        {
            ResponseAPI<UsuarioTokenVM> response = await _usuarioService.ConfirmarEmail(userId, confirmationToken);

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return Ok(response.Data);
        }

        /// <summary>
        /// Alteração da foto perfil do usuário
        /// </summary>
        /// <param name="arquivo">Arquivo com a imagem do usuário</param>
        /// <returns>Retorna usuário logado</returns>
        /// <response code="200">Retorna usuário logado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">usuário não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/alterar-foto-perfil")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarFotoPerfil([FromForm] IFormFile arquivo)
        {
            ResponseAPI<UsuarioVM> response = await _usuarioService.AlterarFotoPerfil(User.GetUserId(), arquivo);

            if (response.Failure) return StatusCode(response.StatusCode, response);

            return Ok(response.Data);
        }

        /// <summary>
        /// Verificar API
        /// </summary>
        /// <returns>It Works</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("up")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Up()
        {
            return Ok("It Works");
        }



    }
}