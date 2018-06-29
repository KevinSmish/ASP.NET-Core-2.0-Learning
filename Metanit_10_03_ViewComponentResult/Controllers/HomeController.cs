using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_10_03_ViewComponentResult.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
