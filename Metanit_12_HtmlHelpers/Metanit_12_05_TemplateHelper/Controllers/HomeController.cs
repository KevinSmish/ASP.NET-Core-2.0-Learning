using Metanit_12_05_TemplateHelper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_12_05_TemplateHelper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Details()
        {
            Phone phone = new Phone { Id = 1, Name = "Nexus 6P", Price = 49000 };
            return View(phone);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Phone phone)
        {
            return Content($"{phone.Name} - {phone.Price}");
        }
    }
}
