using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="usuarioService">IUsuarioService</param>
        /// <param name="mapper">IMapper</param>
        public UsuariosController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registro de usuário professor
        /// </summary>
        /// <param name="registrarProfessorVM">Dados de cadastro do usuário professor</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/registrar/professor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrarProfessor(RegistrarProfessorVM registrarProfessorVM)
        {
            var professor = _mapper.Map<Professor>(registrarProfessorVM);
            var usuario = _mapper.Map<Usuario>(registrarProfessorVM);

            ResultadoService<Usuario> resultado = await _usuarioService.RegistrarProfessor(professor, usuario);

            if (!resultado.Sucesso) return StatusCode(resultado.StatusCode, resultado);

            return StatusCode(resultado.StatusCode, _mapper.Map<UsuarioVM>(resultado.Dados));
        }



        /// <summary>
        /// Login de usuário
        /// </summary>
        /// <param name="loginUsuarioVM">Dados de login do usuário</param>
        /// <returns>Retorna usuário logado</returns>
        /// <response code="200">Retorna usuário logado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpPost("v1/[controller]/entrar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioTokenVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginUsuarioVM loginUsuarioVM)
        {
            ResultadoService<UsuarioTokenVM> resultado;

            switch (loginUsuarioVM.TipoAutenticacao)
            {
                case TipoAutenticacao.Senha:
                    resultado = await _usuarioService.LoginEmailSenha(loginUsuarioVM.Email, loginUsuarioVM.Senha);
                    break;
                case TipoAutenticacao.RefreshToken:
                    resultado = await _usuarioService.LoginRefreshToken(loginUsuarioVM.Email, loginUsuarioVM.RefreshToken);
                    break;
                default:
                    return BadRequest("Tipo de autenticação inválida");
            }

            if (!resultado.Sucesso) return StatusCode(resultado.StatusCode, resultado);

            return Ok(resultado.Dados);
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
            var resultado = await _usuarioService.Logout(User.GetJWTokenId());

            if (!resultado.Sucesso) return StatusCode(resultado.StatusCode, resultado);

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
        public async Task<ActionResult> ConfirmarEmail([FromQuery] string userId, [FromQuery] string confirmationToken)
        {
            ResultadoService<UsuarioTokenVM> resultado = await _usuarioService.ConfirmarEmail(userId, confirmationToken);

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Esqueci a senha
        /// </summary>
        /// <param name="esqueciSenhaVM">Email para redefinir senha</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">usuário não encontrado</response>
        /// <response code="500">Ops, ocorreu um erro no sistema!</response>
        [HttpGet("v1/[controller]/esqueci-senha")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EsqueciSenha(EsqueciSenhaVM esqueciSenhaVM)
        {
            var resultado = await _usuarioService.EsqueciSenha(esqueciSenhaVM);

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return NoContent();
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
        [HttpPatch("v1/[controller]/alterar-foto-perfil")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarFotoPerfil([FromForm] IFormFile arquivo)
        {
            ResultadoService<Usuario> resultado = await _usuarioService.AlterarFotoPerfil(User.GetUserId(), arquivo);

            if (resultado.Falhou) return StatusCode(resultado.StatusCode, resultado);

            return Ok(_mapper.Map<UsuarioVM>(resultado.Dados));
        }


    }
}