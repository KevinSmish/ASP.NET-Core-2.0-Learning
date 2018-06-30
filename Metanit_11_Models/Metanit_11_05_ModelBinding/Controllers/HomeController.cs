using Metanit_11_05_ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_11_05_ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        // https://localhost:44357/Home/AddUser?HasRight=true
        public IActionResult AddUser(User user)
        {
            string userInfo = $"Id: {user.Id}  Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
            return Content(userInfo);
        }

        // https://localhost:44357/Home/AddUser1?Name=Tom&HasRight=true
        public IActionResult AddUser1(User user)
        {
            if (ModelState.IsValid)
            {
                string userInfo = $"Id: {user.Id}  Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
                return Content(userInfo);
            }
            return Content($"Количество ошибок: {ModelState.ErrorCount}");
        }

        // Атрибут Bind позволяет установить выборочную привязку отдельных значений. Так, применим атрибут в методе AddUser2:
        public IActionResult AddUser2([Bind("Name", "Age", "HasRight")] User user)
        {
            string userInfo = $"Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
            return Content(userInfo);
        }

        // В качестве параметра в атрибут Bind передается набор свойств объекта User, которые будут
        // участвовать в процессе привязки. Здесь перечислены все свойства. Но, допустим, уберем пару
        // свойств. Теперь в привязке участвует только свойство Name, поэтому даже если в запросе мы
        // передадим значения для всех остальных свойств, эти значения учитываться не будут, а для
        // соответствующих свойств, не участвующих в привязке, будут применяться значения по умолчанию
        public IActionResult AddUser3([Bind("Name")] User user)
        {
            string userInfo = $"Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
            return Content(userInfo);
        }

        public IActionResult GetUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Content(userAgent);
        }

        // *********************************************
        public IActionResult AddUser4()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser4([FromQuery] User user)
        {
            string userInfo = $"Name: {user.Name}  Age: {user.Age}";
            return Content(userInfo);
        }
        // *********************************************

        public IActionResult Index()
        {
            return View();
        }
    }
}
