using Metanit_07_02_ControllerParameter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Metanit_07_02_ControllerParameter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("User-Agent") &&
                Regex.IsMatch(context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(), "MSIE 8.0"))
            {
                context.Result = Content("Internet Explorer 8.0 не поддерживается");
            }
            base.OnActionExecuting(context);
        }

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

        // https://localhost:44324/Home/GetFile
        public IActionResult GetFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/TextFile.txt");
            // Тип файла - content-type
            //string file_type = "application/txt";
            string file_type = "application/octet-stream";
            // Имя файла - необязательно
            string file_name = "book.txt";
            return PhysicalFile(file_path, file_type, file_name);
        }

        // https://localhost:44324/Home/IndexSix
        public void IndexSix()
        {
            string table = "";
            foreach (var header in Request.Headers)
            {
                table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
            }
            Response.WriteAsync(String.Format("<table>{0}</table>", table));
        }

        // https://localhost:44324/Home/IndexSeven
        public void IndexSeven()
        {
            Response.StatusCode = 404;
            Response.WriteAsync("Ресурс не найден");
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

/*
Объект HttpContext инкапсулирует всю информацию о запросе. В частности, он определяет следующие свойства:

    Request: содержит собственно информацию о текущем запросе.
    Response: управляет ответом
    User: представляет текущего пользователя, который обращается к приложению
    Session: объект для работы с сессиями

Свойство HttpContext.Request представляет объект HttpRequest и предоставляет разнообразную информацию о запросе. Этот же объект доступен через свойство Request класса Conroller. Среди свойств объекта Request можно выделить следующие:
    Body: объект Stream, который используетя для чтения данных запроса
    Cookies: куки, полученные в запросе
    Form: коллекция значений отправленных форм
    Headers: коллекция заголовков запроса
    Path: возвращает запрошенный путь - строка запроса без домена и порта
    Query: возвращает коллекцию переданных через строку запроса параметров
    QueryString: возвращает ту часть запроса, которая содержит параметры. Например, в запросе http://localhost:52682/Home/Index?alt=4 это будет ?alt=4

Свойство HttpContext.Response представляет объект HttpResponse и позволяет управлять ответом на запрос, в частности, устанавливать заголовки ответа, куки, отправлять в выходной поток некоторый ответ. Этот же объект доступен через свойство Response класса Conroller. Среди свойств объекта Response можно выделить следующие:
    Body: объект Stream, который применяется для отправки данных в ответ пользователю
    Cookies: куки, отправляемые в ответе
    ContentType: MIME-тип ответа
    Headers: коллекция заголовков ответа
    StatusCode: статусный код ответа

 */
