using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }        

        [HttpPost("v1/[controller]/registrar-usuario")]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            ResponseAPI<UsuarioVM> response = await _usuarioService.RegistrarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPost("v1/[controller]/entrar")]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginUsuarioVM usuarioVM)
        {
            ResponseAPI<UsuarioVM> response = await _usuarioService.LogarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPost("v1/[controller]/sair")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var response = await _usuarioService.Logout();

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return Ok();
        }


    }
}