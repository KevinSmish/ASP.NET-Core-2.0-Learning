using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace Metanit_05_01_RouterMiddleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // собственно обработчик маршрута
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello ASP.NET Core!");
        }

        /*
         Класс RouteHandler представляет встроенный обработчик маршрута. 
         В качестве параметра в него передается делегат RequestDelegate, 
         который и будет обрабатывать запрос. В данном случае на место делегата 
         передается ссылка на метод Handle(). Метод, который представляет делегат 
         RequestDelegate, должен в качестве параметра принимать контекст запроса 
         HttpContext и возвращать объект Task. В данном случае этот метод просто
         направляет в выходной поток строку "Hello ASP.NET Core!"

        Для построения маршрута применяется объект RouteBuilder. В его конструктор 
        передается сервис IApplicationBuilder, который в данном случае мы можем 
        получить из параметра метода Configure(), и вышеопределенный обработчик маршрута.

        Далее идет собственно определение самого маршрута в методе routeBuilder.MapRoute(). 
        В данном случае "default" задает имя маршрута, а строка "{controller}/{action}" 
        представляет шаблон маршрута - некоторый шаблон Url, с которым будет сопоставляться 
        запрошенный адрес URL. Шаблон URL может состоять из одного и более сегментов. 
        Если в шаблоне используется несколько сегментов, то они разделяются слешами.

        Каждый такой сегмент шаблона содержит параметр. Эти параметры называются параметрами 
        маршрута. Каждый параметр заключается в фигурные скобки. В данном случае это
        параметры controller и action. Но вообще параметры не обязательно должны иметь
        именно такие названия, они могут иметь различные имена, включающие любые
        алфавитно-цифровые символы.

        И последняя строка подключает RouterMiddleware в конвейер обработки запроса:
        app.UseRouter(routeBuilder.Build());

        Вызов routeBuilder.Build() возвращает объект IRouter, который затем переходит
        в RouterMiddleware и используется для обработки запросов.

        После этого обработчик маршрута готов к использованию и может участвовать
        в обработке входящих запросов.
        */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // определяем обработчик маршрута
            var myRouteHandler = new RouteHandler(Handle);
            // создаем маршрут, используя обработчик
            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            // само определение маршрута - он должен соответствовать запросу {controller}/{action}
            routeBuilder.MapRoute("default", "{controller}/{action}");
            // строим маршрут
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
