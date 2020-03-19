using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Usuarios;
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

        //[Route("v1/[controller]")]
        //[AllowAnonymous]
        //[HttpGet]
        //[ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        //public async Task<ActionResult> GenerateToken()
        //{
        //    var token = _tokenService.GerarJWT();

        //    return Ok(token);
        //}

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
            //return CreatedAtAction(nameof(GetInstituicao), new { id = responseVM.Id }, responseVM);
        }

        //[Route("v1/[controller]/entrar")]
        //[AllowAnonymous]
        //[HttpPost]
        //[ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> Login(LoginUsuarioVM usuarioVM)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        //    var result = await _signInManager.PasswordSignInAsync(
        //        usuarioVM.Email, usuarioVM.Password, isPersistent: false, lockoutOnFailure: true
        //    );

        //    if (!result.Succeeded) return BadRequest(new { Message = "Usuário e/ou senha inválidos" } );

        //    return Ok(_tokenService.GerarJWT());
        //}

        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok();
        //}


    }
}