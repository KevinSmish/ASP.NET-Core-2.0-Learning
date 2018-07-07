using Metanit_14_06_DataAnnotations.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_14_06_DataAnnotations.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Person p = new Person
            {
                Name = "Элронд Смит",
                DateOfBirth = new DateTime(2000, 7, 24),
                HomePage = "www.microsoft.com",
                Email = "elrond.smith@gmail.com",
                Password = "qwerty"
            };
            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            return View(person);
        }
    }
}