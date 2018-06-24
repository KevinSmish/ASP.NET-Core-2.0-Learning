using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_08_08_ViewForms.Controllers
{
    // https://localhost:44322/home/login

    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password, bool isMarried, string color, string phone)
        {
            string authData = $"Login: {login}, Password: {password}, isMarried: {isMarried}, color: {color}, phone: {phone}";
            return Content(authData);
        }

        [HttpGet]
        public IActionResult Phones()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Phones(string[] phones)
        {
            string result = "";
            foreach (string p in phones)
            {
                result += p;
                result += ";";
            }
            result = "Вы выбрали: " + result;
            return Content(result);
        }
    }
}
