using Metanit_09_03_Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_09_03_Areas.Controllers
{
    public class MySweetHomeController: Controller
    {
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

        public IActionResult About()
        {
            string contentUrl = Url.Content("~/lib/jquery/dist/jquery.js");
            string actionUrl = Url.Action("Index", "Home");
            return Content(actionUrl);
        }
    }
}
