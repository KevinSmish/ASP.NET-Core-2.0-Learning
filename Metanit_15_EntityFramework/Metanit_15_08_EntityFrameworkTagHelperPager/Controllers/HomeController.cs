using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_15_08_EntityFrameworkTagHelperPager.Models;
using Microsoft.EntityFrameworkCore;

namespace Metanit_15_08_EntityFrameworkTagHelperPager.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;
        public HomeController(UsersContext context)
        {
            db = context;
            if (context.Companies.Count() == 0)
            {
                context.Companies.Add(new Company { Name = "Oracle" });
                context.Companies.Add(new Company { Name = "Microsoft" });
                context.Companies.Add(new Company { Name = "Google" });
                context.Companies.Add(new Company { Name = "Apple" });
                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                context.Users.Add(new User { Name = "Станислав Иванов", Age = 25, CompanyId = 1 });
                context.Users.Add(new User { Name = "Федор Петров", Age = 32, CompanyId = 1 });
                context.Users.Add(new User { Name = "Олег Сидоров", Age = 19, CompanyId = 2 });
                context.Users.Add(new User { Name = "Олег Кузнецов", Age = 29, CompanyId = 3 });
                context.Users.Add(new User { Name = "Василий Иванов", Age = 37, CompanyId = 4 });
                context.Users.Add(new User { Name = "Андрей Петров", Age = 24, CompanyId = 4 });
                context.Users.Add(new User { Name = "Александр Овсов", Age = 36, CompanyId = 3 });
                context.Users.Add(new User { Name = "Иван Иванов", Age = 42, CompanyId = 3 });
                context.Users.Add(new User { Name = "Петр Андреев", Age = 47, CompanyId = 3 });
                context.Users.Add(new User { Name = "Сергей Сидоров", Age = 51, CompanyId = 4 });
                context.Users.Add(new User { Name = "Иван Охлобыстин", Age = 25, CompanyId = 1 });
                context.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;
            IQueryable<User> source = db.Users.Include(x => x.Company);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
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
