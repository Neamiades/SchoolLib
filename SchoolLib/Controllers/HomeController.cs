using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolLib.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Тут Ви можете ознайомитися з метою нашої системи.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Ви можете зв'язатися з нами використовуючи інформацію на цій сторінці.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
