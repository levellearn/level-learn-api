using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IAlunoService _alunoService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="usuarioService"></param>
        /// <param name="alunoService"></param>
        public ProfessoresController(IMapper mapper, IUsuarioService usuarioService, IAlunoService alunoService)
            : base(mapper)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Registro de usuário professor
        /// </summary>
        /// <param name="registrarProfessorVM">Dados de cadastro do usuário professor</param>
        /// <returns>Sem conteúdo</returns>     
        [HttpPost("v1/[controller]/registrar/professor")]
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


    }
}
