﻿using Microsoft.AspNetCore.Mvc;

namespace LevelLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }        
    }
}
