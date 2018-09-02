using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }        
    }
}
