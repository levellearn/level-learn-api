using AutoMapper;
using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.Web.Extensions.Common;
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
    public class InstituicoesController : Controller
    {
        private readonly IInstituicaoService _instituicaoService;
        private readonly IPessoaService _pessoaService;
        private readonly UserManager<ApplicationUser> _userManager;
        public InstituicoesController(IInstituicaoService instituicaoService, IPessoaService pessoaService, UserManager<ApplicationUser> userManager)
        {
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
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(CreateInstituicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Instituicao instituicao = Mapper.Map<Instituicao>(viewModel);

            List<StatusResponseEnum> status = _instituicaoService.ValidaInstituicao(instituicao);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            ApplicationUser user = Task.Run(() => _userManager.GetUserAsync(User)).Result;
            viewModel.Admins.Add(user.PessoaId);

            if (_instituicaoService.Insert(instituicao, viewModel.Admins, viewModel.Professores, viewModel.Alunos))
                return Json(new { MensagemSucesso = "Instituição inclusa com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao adicionar instituição" });
        }

        [HttpGet]
        public IActionResult CarregaUpdate(int id)
        {
            Instituicao instituicao = _instituicaoService.SelectById(id);

            if (instituicao == null)
                return PartialView("_Create");

            UpdateInstituicaoViewModel viewModel = Mapper.Map<UpdateInstituicaoViewModel>(instituicao);

            return PartialView("_Update", viewModel);
        }

        [HttpPost]
        public IActionResult Update(UpdateInstituicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            Instituicao instituicao = Mapper.Map<Instituicao>(viewModel);

            List<StatusResponseEnum> status = _instituicaoService.ValidaInstituicao(instituicao);

            if (status.Count > 0)
                return Json(new { MensagemErro = status.DisplayDescriptionsToViewModel() });

            if (_instituicaoService.Update(instituicao))
                return Json(new { MensagemSucesso = "Instituição atualizada com sucesso" });
            else
                return Json(new { MensagemErro = "Erro ao atualizar instituição" });
        }

        [HttpGet]
        public IActionResult Lista()
        {
            IEnumerable<ViewInstituicaoViewModel> viewModels = Mapper.Map<IEnumerable<ViewInstituicaoViewModel>>(_instituicaoService.Select().OrderBy(p => p.Nome));
            return PartialView("_List", viewModels);
        }
    }
}