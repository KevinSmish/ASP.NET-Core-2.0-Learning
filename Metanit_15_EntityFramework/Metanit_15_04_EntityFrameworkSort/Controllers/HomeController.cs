using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_15_04_EntityFrameworkSort.Models;
using Microsoft.EntityFrameworkCore;

namespace Metanit_15_04_EntityFrameworkSort.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;
        public HomeController(UsersContext context)
        {
            this.db = context;

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

        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<User> users = db.Users.Include(x => x.Company);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            return View(await users.AsNoTracking().ToListAsync());
        }

        /*
        public IActionResult Index()
        {
            return View();
        }
        */

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
