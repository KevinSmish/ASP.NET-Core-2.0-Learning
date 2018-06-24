using Microsoft.AspNetCore.Mvc;

namespace Metanit_08_09_ViewEngine.Controllers
{
    public class HomeController : Controller 
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View("About");
        }

        public ViewResult Contact()
        {
            return View();
        }
    }
}
