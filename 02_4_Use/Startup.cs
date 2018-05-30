using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _02_4_Use
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /* Вариант 1 
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            int x = 5;
            int y = 8;
            int z = 0;

            // В данном случае мы используем перегрузку метода Use, которая в качестве параметров
            // принимает контекст запроса - объект HttpContext и делегат Func<Task>, который 
            //представляет собой ссылку на следующий в конвейере компонент middleware.

            // Метод app.Use реализует простейшую задачу - умножение двух чисел и затем передает
            // обработку запроса следующим компонентам middleware в конвейере.

            // То есть при вызове await next.Invoke() обработка запроса перейдет к тому компоненту,
            // который установлен в методе app.Run()

            app.Use(async (context, next) =>
            {
                z = x * y;

                // Если бы мы не использовали вызов await next.Invoke() 
                // или закомментировали бы его, то обращения к следующему компоненту 
                // в конвейере не произошло бы.
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"x * y = {z}");
                //await context.Response.WriteAsync("Hello World!");
            });
        }
        */

        // Вариант 2
        public void Configure(IApplicationBuilder app)
        {
            int x = 2;
            /*
            app.Map("/index", Index);
            app.Map("/about", About);
            */

            // Метод Map может иметь вложенные методы Map, которые обрабатывают подмаршруты. 
            app.Map("/home", home =>
            {
                home.Map("/index", Index);
                home.Map("/about", About);
            });

            // Похожим образом работает метод MapWhen(). Он принимает в качестве параметра 
            // делегат Func<HttpContext, bool> и обрабатывает запрос, если функция, 
            // передаваемая в качестве параметра возвращает true.

            app.MapWhen(context => {

                return context.Request.Query.ContainsKey("id") &&
                        context.Request.Query["id"] == "5";
            }, HandleId);

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<p>Hello world!</p>");
                x = x * 2;      // 2 * 2 = 4
                await next.Invoke();    // вызов app.Run
                x = x * 2;      // 8 * 2 = 16
                await context.Response.WriteAsync($"Result: {x}");
            });

            app.Run(async (context) =>
            {
                // await Task.Delay(10000); можно поставить задержку
                await context.Response.WriteAsync("<p>Good bye, World...</p>");

                x = x * 2;  //  4 * 2 = 8
                await Task.FromResult(0);
            });
        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Index");
            });
        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("About");
            });
        }

        // В данном случае если в запросе указан параметр id и он имеет значение 5, то запрос 
        // обрабатывается функцией HandleId(). К подобным запросам будут относиться, например, 
        // запрос http://localhost:55234/?id=5 или 
        // http://localhost:55234/product?id=5&name=phone, так как обе строки запроса содержат 
        // параметр id равный 5. А все остальные запросы также будут обрабатываться делегатом, 
        // передаваемым в метод app.Run().

        private static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("id is equal to 5");
            });
        }

    }
}
