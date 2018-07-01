using Metanit_12_04_StrongTypeHtmlHelper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_12_04_StrongTypeHtmlHelper.Controllers
{
    public class HomeController : Controller
    {
        List<Phone> phones;

        public HomeController()
        {
            phones = new List<Phone>
            {
                new Phone { Id=1, Name="iPhone 6S", Price=56000 },
                new Phone { Id=2, Name="iPhone 5S", Price=41000 },
                new Phone { Id=3, Name="Lumia 550", Price=9000 },
                new Phone { Id=4, Name="Lumia 950", Price=40000 },
                new Phone { Id=5, Name="Nexus 5X", Price=30000 },
                new Phone { Id=6, Name="Nexus 6P", Price=50000 }
            };
        }

        public IActionResult Index()
        {
            return View(phones);
        }

        // *********************************************
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromQuery] Phone phone)
        {
            string phoneInfo = $"Name: {phone.Name}  Price: {phone.Price}";
            return Content(phoneInfo);
        }

    }
}
