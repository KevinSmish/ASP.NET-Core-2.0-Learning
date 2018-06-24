using Metanit_08_02_Razor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_08_02_Razor.Controllers
{
    public class HomeController: Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public HomeController(IHostingEnvironment appEnvironment) 
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            Product[] array = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            return View(array);
        }

        // https://localhost:44344/home/about
        public IActionResult About()
        {
            ViewData["Message"] = "Hello ASP.NET Core";
            ViewBag.Message1 = "Hello ASP.NET Core (from ViewBag)";
            ViewBag.Countries = new List<string> { "Бразилия", "Аргентина", "Уругвай", "Чили" };

            return View();
        }

        // https://localhost:44344/home/about1
        public IActionResult About1()
        {
            List<string> countries = new List<string> { "Бразилия", "Аргентина", "Уругвай", "Чили" };
            return View(countries);
        }

        public ActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }
    }
}
