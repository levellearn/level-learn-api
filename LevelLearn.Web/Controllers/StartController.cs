using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.Web.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}