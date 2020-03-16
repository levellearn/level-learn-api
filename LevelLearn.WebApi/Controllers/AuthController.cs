using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelLearn.Service.Services.Auth;
using LevelLearn.ViewModel.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Route("v1/[controller]")]
        [HttpGet]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        public async Task<ActionResult> GenerateToken()
        {
            var token = _tokenService.GenerateToken();

            return Ok(token);
        }

    }
}