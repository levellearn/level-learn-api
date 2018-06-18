using LevelLearn.ViewModel.Pessoas.Pessoa;
using LevelLearn.Web.Extensions.Common;
using LevelLearn.Web.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logar(LoginPessoaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { MensagemErro = ModelState.DisplayErros() });

            ApplicationUser user = await _userManager.Users.Include(p => p.Pessoa).Where(p => p.UserName == viewModel.Email).FirstOrDefaultAsync();

            if (user == null)
                return Json(new { MensagemErro = "Usuário e senha inválidos ;(" });

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, viewModel.Senha, true, false);

            if (!result.Succeeded)
                return Json(new { MensagemErro = "Usuário e senha inválidos ;(" });

            var retorno = new
            {
                user.Pessoa.Imagem,
                user.Pessoa.UserName
            };

            return Json(new { MensagemSucesso = "Login realizado com sucesso, estamos te direcionando para o jogo", Retorno = retorno });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Start");
        }
    }
}