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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }        

        [Route("v1/[controller]/registrar-usuario")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioVM usuarioVM)
        {
            ResponseAPI response = await _usuarioService.RegistrarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response.Data);
        }

        [Route("v1/[controller]/entrar")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginUsuarioVM usuarioVM)
        {
            ResponseAPI response = await _usuarioService.LogarUsuario(usuarioVM);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response.Data);
        }

        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok();
        //}


    }
}