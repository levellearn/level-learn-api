using AutoMapper;
using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.ViewModel.Enum;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.Web.Extensions.Common;
using LevelLearn.Web.Extensions.Services.Institucional;
using LevelLearn.Web.Extensions.Services.Pessoas;
using LevelLearn.Web.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Web.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoService _cursoService;
        private readonly IInstituicaoService _instituicaoService;
        private readonly IPessoaService _pessoaService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CursosController(ICursoService cursoService, IInstituicaoService instituicaoService, IPessoaService pessoaService, UserManager<ApplicationUser> userManager)
        {
            _cursoService = cursoService;
            _instituicaoService = instituicaoService;
            _pessoaService = pessoaService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult Index() => View();

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult CarregaCreate()
        {
            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            ViewBag.DropDownListProfessores = _pessoaService.SelectListProfessoresWithoutUser(user.PessoaId);
            ViewBag.DropDownListAlunos = _pessoaService.SelectListAlunosWithoutUser(user.PessoaId);
            ViewBag.DropDownListInstituicoes = _instituicaoService.SelectListInstiuicoesProfessor(user.PessoaId);
            return PartialView("_Create");
        }

        [HttpPost]
        [Authorize(Roles = "PROF")]
        public IActionResult Create(CreateCursoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Curso curso = Mapper.Map<Curso>(viewModel);

            List<StatusResponseEnum> status = _cursoService.ValidaCurso(curso);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;
            viewModel.Professores.Add(user.PessoaId);

            if (_cursoService.Insert(curso, viewModel.Professores, viewModel.Alunos))
                return Json(new { MensagemSucesso = "Curso incluso com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao adicionar curso" });
        }

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult CarregaUpdate(int id)
        {
            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            ViewBag.DropDownListInstituicoes = _instituicaoService.SelectListInstiuicoesAdmin(user.PessoaId);

            if (!_cursoService.IsProfessorDoCurso(id, user.PessoaId))
            {
                ViewBag.DropDownListProfessores = _pessoaService.SelectListProfessoresWithoutUser(user.PessoaId);
                ViewBag.DropDownListAlunos = _pessoaService.SelectListAlunosWithoutUser(user.PessoaId);
                return PartialView("_Create");
            }

            Curso curso = _cursoService.SelectById(id);

            if (curso == null)
            {
                ViewBag.DropDownListProfessores = _pessoaService.SelectListProfessoresWithoutUser(user.PessoaId);
                ViewBag.DropDownListAlunos = _pessoaService.SelectListAlunosWithoutUser(user.PessoaId);
                return PartialView("_Create");
            }

            UpdateCursoViewModel viewModel = Mapper.Map<UpdateCursoViewModel>(curso);

            return PartialView("_Update", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "PROF")]
        public IActionResult Update(UpdateCursoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Curso curso = Mapper.Map<Curso>(viewModel);

            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            if (!_cursoService.IsProfessorDoCurso(curso.CursoId, user.PessoaId))
                return Json(new { MensagemErro = "Você não é professor desse cursto" });

            List<StatusResponseEnum> status = _cursoService.ValidaCurso(curso);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            if (_cursoService.Update(curso))
                return Json(new { MensagemSucesso = "Curso alterado com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao alterar curso" });
        }

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult Lista()
        {
            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            List<Curso> cursos = _cursoService.CursosInstituicaoProfessor(user.PessoaId);
            List<ViewCursoViewModel> viewModels = Mapper.Map<List<ViewCursoViewModel>>(cursos);

            viewModels.ForEach(p => p.IsProfessor = p.Pessoas.Where(x => x.Perfil == TipoPessoaEnumViewModel.Professor && x.PessoaId == user.PessoaId).Count() > 0);
            return PartialView("_List", viewModels);
        }
    }
}