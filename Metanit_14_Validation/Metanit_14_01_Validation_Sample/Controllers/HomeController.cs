using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_14_01_Validation_Sample.Models;

namespace Metanit_14_01_Validation_Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            // Если данные для модели были введены правильно, то через метод Content() 
            // эти данные будут выведены в браузере. Если же были введены некорректные данные, 
            // то возвращаем объект Person в метод View.

            // Валидация на стороне сервера. 
            // Если у пользователя отключен javascript а браузере, или в представлении
            // не добавлены скрипты клиентской валидации, то форма благополучно отправится
            // на сервер. Но во фреймворке предусмотрена также валидация на стороне сервера:
            if (ModelState.IsValid)
            {
                //if (string.IsNullOrEmpty(person.Name))
                if (person.Name == "Вася")
                {
                    ModelState.AddModelError("Name", "Некорректное имя");
                    return View(person);
                }

                if (person.Name == person.Password)
                {
                    ModelState.AddModelError("", "Имя и пароль не должны совпадать");
                    return View(person);
                }

                return Content($"{person.Name} - {person.Email}");
            }
            else
                return View(person);
        }
        // ---------------------------------------------------------------------
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            if (email == "admin@mail.ru" || email == "aaa@gmail.com")
                return Json(false);
            return Json(true);
        }
        // ---------------------------------------------------------------------
        public IActionResult Index()
        {
            return View();
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
