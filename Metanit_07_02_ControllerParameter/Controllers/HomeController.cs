using Metanit_07_02_ControllerParameter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_07_02_ControllerParameter.Controllers
{
    public class HomeController : Controller
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

        // https://localhost:44324/Home/Hello?id=9
        public string Hello(int id)
        {
            return $"id= {id}";
        }
    }
}
