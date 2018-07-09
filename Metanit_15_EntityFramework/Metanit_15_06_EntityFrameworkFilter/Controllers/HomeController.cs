using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_15_06_EntityFrameworkFilter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Metanit_15_06_EntityFrameworkFilter.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;
        public HomeController(UsersContext context)
        {
            db = context;
            /*
                if (context.Companies.Count() == 0)
                {
                    context.Companies.Add(new Company { Name = "Oracle" });
                    context.Companies.Add(new Company { Name = "Microsoft" });
                    context.SaveChanges(); 
                }

                if (context.Users.Count() == 0)
                {
                    context.Users.Add(new User { Name = "Иванов", Age = 25, CompanyId = 1 });
                    context.Users.Add(new User { Name = "Петров", Age = 32, CompanyId = 1 });
                    context.Users.Add(new User { Name = "Сидоров", Age = 19, CompanyId = 2 });
                    context.SaveChanges();
                }
            */
        }
        public ActionResult Index(int? company, string name)
        {
            IQueryable<User> users = db.Users.Include(p => p.Company);
            if (company != null && company != 0)
            {
                users = users.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            List<Company> companies = db.Companies.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Company { Name = "Все", Id = 0 });

            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = users.ToList(),
                Companies = new SelectList(companies, "Id", "Name"),
                Name = name
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

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
