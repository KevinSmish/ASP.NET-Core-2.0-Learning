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
                string name = p?.Name;
                decimal? price = p?.Price;
                string relatedName = p?.Related?.Name;

                results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}",
                    name, price, relatedName));
            }
            return View(results);
        }
    }
}