using Microsoft.AspNetCore.Mvc;
using System.Linq;
//using System.Collections.Generic;
using AdamFreeman_Ch04_Essential_CSharp_Features.Models;
//using System;

namespace AdamFreeman_Ch04_Essential_CSharp_Features.Controllers
{
    public class HomeController : Controller
    {
        /*
        public ViewResult Index()
        {

            //// 1. Comment
            //List<string> results = new List<string>();
            //foreach (Product p in Product.GetProducts())
            //{
            //    // string name = p?.Name ?? "<No Name>";
            //    // decimal? price = p?.Price ?? 0;
            //    // string relatedName = p?.Related?.Name ?? "<None>";

            //    // Old style working with a string
            //    //results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}",
            //    //    name, price, relatedName));

            //    // New style working with a string
            //    //results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");

            //    Dictionary<string, Product> products = new Dictionary<string, Product>
            //    {
            //        ["Kayak"] = new Product { Name = "Kayak", Price = 275M },
            //        ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
            //    };
            //    return View("Index", products.Keys);
            //}

            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            // 2. Comment
            //bool FilterByPrice(Product p)
            //{
            //    return (p?.Price ?? 0) >= 20;
            //}

            // 3. Comment
            //Func<Product, bool> nameFilter = delegate (Product prod) {
            //    return prod?.Name?[0] == 'S';
            //};
            

            decimal priceFilterTotal = productArray
                .Filter(p => (p?.Price ?? 0) >= 20)
                .TotalPrices();

            decimal nameFilterTotal = productArray
                .Filter(p => p?.Name?[0] == 'S')
                .TotalPrices();

            // 4. Comment
            //decimal cartTotal = cart.TotalPrices();
            //decimal arrayTotal = productArray.TotalPrices();
            //return View("Index", new string[] {
            //    $"Cart Total: {cartTotal:C2}",
            //    $"Array Total: {arrayTotal:C2}" });

            // 5. Comment
            //decimal arrayTotal = productArray.FilterByPrice(20).TotalPrices();
            //return View("Index", new string[] { $"Array Total: {arrayTotal:C2}" });

            // 6. Comment
            //return View("Index", new string[] {
            //    $"Price Total: {priceFilterTotal:C2}",
            //    $"Name Total: {nameFilterTotal:C2}" });

            //return View(Product.GetProducts().Select(p => p?.Name));
        }
        */        

        public ViewResult Index() =>
            View(Product.GetProducts().Select(p => p?.Name));
    }
}