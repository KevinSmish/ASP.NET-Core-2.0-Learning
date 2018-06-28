using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_09_01_MapRoute.Controllers
{
    public class HomeController : Controller
    {
        // https://localhost:44372/homepage
        [Route("homepage")]
        [Route("Main")]     // сопоставляется с homepage либо с Main
        public IActionResult Index()
        {
            return Content("Hello ASP.NET MVC 6");
        }

        // http://localhost:44372/10/lumia
        [Route("{id:int}/{name:maxlength(10)}")]
        public IActionResult Test(int id, string name)
        {
            return Content($" id={id} | name={name}");
        }

        [Route("[controller]/[action]")]
        public IActionResult IndexOne()
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();
            return Content($"controller: {controller} | action: {action}");
        }

        /* 
            [Route("main/store/{name}")]
            public IActionResult Index(string name)
            {
                return Content(name);
            }
            [Route("main/{id:int}/{name:maxlength(10)}")]
            public IActionResult Test(int id, string name)
            {
                return Content($" id={id} | name={name}");
            }
        */
    }
}
