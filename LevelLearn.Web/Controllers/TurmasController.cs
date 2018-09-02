using AutoMapper;
using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.ViewModel.Institucional.Turma;
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
    public class TurmasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITurmaService _turmaService;
        private readonly ICursoService _cursoService;
        private readonly IPessoaService _pessoaService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TurmasController(IMapper mapper, ITurmaService turmaService, ICursoService cursoService, IPessoaService pessoaService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _turmaService = turmaService;
            _cursoService = cursoService;
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

            ViewBag.DropDownListCursos = _cursoService.SelectListCursosProfessor(user.PessoaId);
            return PartialView("_Create");
        }

        [HttpPost]
        [Authorize(Roles = "PROF")]
        public IActionResult Create(CreateTurmaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Turma turma = _mapper.Map<Turma>(viewModel);

            List<StatusResponseEnum> status = _turmaService.ValidaTurma(turma);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;
            turma.ProfessorId = user.PessoaId;

            if (_turmaService.Insert(turma, viewModel.AlunoIds))
                return Json(new { MensagemSucesso = "Turma inclusa com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao adicionar turma" });
        }

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult CarregaUpdate(int id)
        {
            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            ViewBag.DropDownListCursos = _cursoService.SelectListCursosProfessor(user.PessoaId);
            ViewBag.DropDownListAlunos = _pessoaService.SelectListAlunosWithoutUser(user.PessoaId);

            if (!_turmaService.IsTurmaDoProfessor(id, user.PessoaId))
                return PartialView("_Create");

            Turma turma = _turmaService.SelectById(id);

            UpdateTurmaViewModel viewModel = _mapper.Map<UpdateTurmaViewModel>(turma);

            return PartialView("_Update", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "PROF")]
        public IActionResult Update(UpdateTurmaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Turma turma = _mapper.Map<Turma>(viewModel);

            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            if (!_turmaService.IsTurmaDoProfessor(turma.TurmaId, user.PessoaId))
                return Json(new { MensagemErro = "Você não é professor dessa turma" });

            List<StatusResponseEnum> status = _turmaService.ValidaTurma(turma);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            if (_turmaService.Update(turma))
                return Json(new { MensagemSucesso = "Turma alterada com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao alterar turma" });
        }

        [HttpGet]
        [Authorize(Roles = "PROF")]
        public IActionResult Lista()
        {
            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;

            List<Turma> turmas = _turmaService.SelectIncludes(p => p.ProfessorId == user.PessoaId, i => i.Curso).OrderBy(p => p.Nome).ToList();
            List<ViewTurmaViewModel> viewModels = _mapper.Map<List<ViewTurmaViewModel>>(turmas);

            return PartialView("_List", viewModels);
        }
    }
}