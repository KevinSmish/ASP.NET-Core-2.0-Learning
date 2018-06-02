using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

/*
Сессии

Сессия представляет собой ряд последовательных запросов, совершенных в одном браузере в течение некоторого 
времени. Сессия может использоваться для сохранения каких-то временных данных, которые должны быть доступны, 
пока пользователь работает с приложением.

Для хранения состояния сессии на сервере создается словарь или хеш-таблица, которая хранится в кэше и которая 
существует для всех запросов из одного браузера в течение некоторого времени. На клиенте хранится идентификатор
сессии в куках. Этот идентификатор посылается на сервер с каждым запросом. Сервер использует этот идентификатор
для извлечения нужных данных из сессии. Эти куки удаляются только при завершении сессии. Но если сервер получает
куки, которые установлены уже для истекшей сессии, то для этих кук создается новая сессия.

Сервер хранит данные сессии в течение ограниченного промежутка времени после последнего запроса. По умолчанию 
этот промежуток равен 20 минутам, хотя его также можно изменить.

Для работы с сессиями проект ASP.NET Core использует пакеты Microsoft.AspNetCore.Session и 
Microsoft.Extensions.Caching.Memory. Если проект использует версию ASP.NET Core 2.0 и выше, то эти пакеты уже 
есть проекте.

Чтобы использовать сессии, необходимо сконфигурировать их параметры в классе Startup. Все сессии работают поверх
объекта IDistributedCache, и ASP.NET Core предоставляет встроенную реализацию IDistributedCache, которую мы
можем использовать. И для этого изменим метод ConfigureServices():

*/

namespace _02_13_HttpContext
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ******************************************************
            // Вариант 3 Session
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ******************************************************
            // Вариант 1 HttpContext
            /*
            app.Use(async (context, next) =>
            {
                context.Items["text"] = "Text from HttpContext.Items";
                await next.Invoke();
            });
            
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текст: {context.Items["text"]}");
            });
            */

            // ******************************************************
            // Вариант 2 Cookies
            /*
            app.Run(async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("name"))
                {
                    string name = context.Request.Cookies["name"];
                    await context.Response.WriteAsync($"Hello {name}!");
                }
                else
                {
                    context.Response.Cookies.Append("name", "Tom");
                    await context.Response.WriteAsync("Hello World!");
                }
            });
            */

            // ******************************************************
            // Вариант 3 Session
            app.UseSession();
            app.Run(async (context) =>
            {
                if (context.Session.Keys.Contains("name"))
                    await context.Response.WriteAsync($"Hello {context.Session.GetString("name")}!");
                else
                {
                    context.Session.SetString("name", "Tom");
                    await context.Response.WriteAsync("Hello World!");
                }
            });

            /*
                Объект Session определяет ряд свойств и методов, которые мы можем использовать:
                Keys: свойство, представляющее список строк, который хранит все доступные ключи
                Clear(): очищает сессию
                Get(string key): получает по ключу key значение, которое представляет массив байтов
                GetInt32(string key): получает по ключу key значение, которое представляет целочисленное значение
                GetString(string key): получает по ключу key значение, которое представляет строку
                Set(string key, byte[] value): устанавливает по ключу key значение, которое представляет массив байтов
                SetInt32(string key, int value): устанавливает по ключу key значение, которое представляет целочисленное значение value
                SetString(string key, string value): устанавливает по ключу key значение, которое представляет строку value
                Remove(string key): удаляет значение по ключу
            */
        }
    }
}
