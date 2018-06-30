using Metanit_11_01_SimpleModel.Models;
using Metanit_11_01_SimpleModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_11_01_SimpleModel.Controllers
{
    public class HomeController : Controller
    {
        List<Company> companies;
        List<Phone> phones;

        public HomeController()
        {
            Company apple = new Company { Id = 1, Name = "Apple", Country = "США" };
            Company microsoft = new Company { Id = 2, Name = "Microsoft", Country = "США" };
            Company google = new Company { Id = 3, Name = "Google", Country = "США" };
            companies = new List<Company> { apple, microsoft, google };

            phones = new List<Phone>
            {
                new Phone { Id=1, Manufacturer= apple, Name="iPhone 6S", Price=56000 },
                new Phone { Id=2, Manufacturer= apple, Name="iPhone 5S", Price=41000 },
                new Phone { Id=3, Manufacturer= microsoft, Name="Lumia 550", Price=9000 },
                new Phone { Id=4, Manufacturer= microsoft, Name="Lumia 950", Price=40000 },
                new Phone { Id=5, Manufacturer= google, Name="Nexus 5X", Price=30000 },
                new Phone { Id=6, Manufacturer= google, Name="Nexus 6P", Price=50000 }
            };
        }

        /*
        public IActionResult Index()
        {
            return View(phones);
        }
        */

        public IActionResult Index(int? companyId)
        {
            // формируем список компаний для передачи в представление
            List<CompanyModel> compModels = companies
                .Select(c => new CompanyModel { Id = c.Id, Name = c.Name })
                .ToList();
            // добавляем на первое место
            compModels.Insert(0, new CompanyModel { Id = 0, Name = "Все" });

            IndexViewModel ivm = new IndexViewModel { Companies = compModels, Phones = phones };

            // если передан id компании, фильтруем список
            if (companyId != null && companyId > 0)
                ivm.Phones = phones.Where(p => p.Manufacturer.Id == companyId);

            return View(ivm);
        }
    }
}
