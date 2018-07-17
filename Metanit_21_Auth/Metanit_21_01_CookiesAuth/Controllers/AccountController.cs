using Metanit_21_01_CookiesAuth.Models;
using Metanit_21_01_CookiesAuth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Metanit_21_01_CookiesAuth.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        public AccountController(UserContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        /*
            Каждый объект claim представляет класс Claim, который определяет следующие свойства:
                Issuer: "издатель" или название системы, которая выдала данный claim
                Subject: возвращает информацию о пользователе в виде объекта ClaimsIdentity
                Type: возвращает тип объекта claim
                Value: возвращает значение объекта claim

            Для создания claima ему в конструктор передается тип и значения. 
            Тип ClaimsIdentity.DefaultNameClaimType фактически представляет логин. 
            А userName, в данном случае будет представлять значение, которое затем мы сможем 
            получить через выражение User.Identity.Name

            Для работы с объектами Claim в классе ClaimsPrincipal есть следующие свойства и методы:
                Identity: возвращает объект ClaimsIdentity, который реализует интерфейс IIdentity 
                    и представляет текущего пользователя
                FindAll(type) / FindAll(predicate): возвращает все объекты claim, которые
                    соответствуют определенному типу или условию
                FindFirst(type) / FindFirst(predicate): возвращает первый объект claim, 
                    который соответствуют определенному типу или условию
                HasClaim(type, value) / HasClaim(predicate): возвращает значение true, если 
                    пользователь имеет claim определенного типа с определенным значением
                IsInRole(name): возвращает значение true, если пользователь принадлежит роли 
                    с названием name

            С помощью объекта ClaimsIdentity, который возвращается свойством User.Identity, 
            мы можем управлять объектами claim у текущего пользователя. В частности, класс 
            ClaimsIdentity определяет следующие свойства и методы:
                Claims: свойство, которое возвращает набор ассоциированных с пользователем объектов claim
                AddClaim(claim): добавляет для пользователя объект claim
                AddClaims(claims): добавляет набор объектов claim
                FindAll(predicate): возвращает все объекты claim, которые соответствуют 
                    определенному условию
                HasClaim(predicate): возвращает значение true, если пользователь имеет claim, 
                    соответствующий определенному условию
                RemoveClaim(claim): удаляет объект claim

            Для создания объекта ClaimsIdentity в его конструктор передается набор claim, 
            тип аутентификации (ApplicationCookie), тип для claima, представляющего логин, 
            и тип для claima, представляющего роль.

            Созданный объект ClaimsIdentity передается в конструктор ClaimsPrincipal. 
            И фактически этот объект ClaimsPrincipal и будет представлять то, что мы потом 
            в любом контроллере сможем получить через HttpContext.User.

        */
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
