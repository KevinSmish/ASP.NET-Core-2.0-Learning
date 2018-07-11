using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_17_01_IMemoryCache.Models;
using Metanit_17_01_IMemoryCache.Services;

namespace Metanit_17_01_IMemoryCache.Controllers
{
    public class HomeController : Controller
    {
        ProductService productService;
        public HomeController(ProductService service)
        {
            productService = service;
            productService.Initialize();
        }
        public async Task<IActionResult> Index(int id)
        {
            Product product = await productService.GetProduct(id);
            if (product != null)
                return Content($"Product: {product.Name}");
            return Content("Product not found (use https://localhost:44357/home/index/1)");
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
