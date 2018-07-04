using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<Company> companies = new List<Company>
        {
            new Company { Id = 1, Name = "Apple" },
            new Company { Id = 2, Name = "Samsung" },
            new Company { Id=3, Name="Microsoft" }
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        /*
        [HttpPost]
        public string Create(Phone phone)
        {
            Company company = companies.FirstOrDefault(c => c.Id == phone.CompanyId);
            return $"Добавлен новый элемент: {phone.Name} ({company?.Name})";
        }
        */

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
