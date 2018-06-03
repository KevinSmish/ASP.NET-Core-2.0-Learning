using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AdamFreeman_Ch04_Essential_CSharp_Features.Models;

namespace AdamFreeman_Ch04_Essential_CSharp_Features.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            List<string> results = new List<string>();
            foreach (Product p in Product.GetProducts())
            {
                // string name = p?.Name ?? "<No Name>";
                // decimal? price = p?.Price ?? 0;
                // string relatedName = p?.Related?.Name ?? "<None>";

                // Old style working with a string
                //results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}",
                //    name, price, relatedName));

                // New style working with a string
                //results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");

                Dictionary<string, Product> products = new Dictionary<string, Product> {
                    ["Kayak"] = new Product { Name = "Kayak", Price = 275M },
                    ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
                };
                return View("Index", products.Keys);

            }
            return View(results);
        }
    }
}