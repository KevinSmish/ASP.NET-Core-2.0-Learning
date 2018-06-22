using Metanit_07_02_ControllerParameter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_07_02_ControllerParameter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Product[] array = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            return View(array);
        }

        public IActionResult Index1()
        {
            return View();
        }

        // https://localhost:44324/Home/Hello?id=9
        public string Hello(int id)
        {
            return $"id= {id}";
        }

        // https://localhost:44324/Home/Square?a=10&h=3
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return $"Площадь треугольника с основанием {a} и высотой {h} равна {s}";
        }

        // https://localhost:44324/Home/SquareOne?Altitude=10&Height=3
        public string SquareOne(Geometry geometry)
        {
            return $"!Площадь треугольника с основанием {geometry.Altitude} и высотой {geometry.Height} равна {geometry.GetSquare()}";
        }

        [HttpPost]
        public string SquareTwo(int altitude, int height)
        {
            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        }

        // https://localhost:44324/Home/GetUser
        public JsonResult GetUser()
        {
            User user = new User { Name = "Tom", Age = 28 };
            return Json(user);
        }

        // https://localhost:44324/Home/About
        public IActionResult About()
        {
            return Redirect("~/Home/Index");
        }

        // https://localhost:44324/Home/AboutTwo
        public IActionResult AboutTwo()
        {
            return RedirectToAction("SquareOne", "Home", new { altitude = 10, height = 3 });
        }

        // https://localhost:44324/Home/IndexThree
        public IActionResult IndexThree()
        {
            return StatusCode(401);
        }

        // https://localhost:44324/Home/IndexFour
        public IActionResult IndexFour()
        {
            return NotFound("Ресурс в приложении не найден");
        }

        // https://localhost:44324/Home/IndexFive
        // https://localhost:44324/Home/IndexFive?s='Me'
        public IActionResult IndexFive(string s)
        {
            if (String.IsNullOrEmpty(s))
                return BadRequest("Не указаны параметры запроса");
            return Content("Ok");
        }

    }

    public class Geometry
    {
        public int Altitude { get; set; } // основание
        public int Height { get; set; } // высота

        public double GetSquare() // вычисление площади треугольника
        {
            return Altitude * Height / 2;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
