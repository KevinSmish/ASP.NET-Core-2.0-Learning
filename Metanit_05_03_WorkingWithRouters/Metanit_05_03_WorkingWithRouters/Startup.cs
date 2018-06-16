using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

/*

    Рассмотрим, как происходит работа с маршрутами. При удачном сопоставлении адреса URL 
    определенному маршруту у объекта RouteContext устанавливаются свойства Handler и RouteData. 
    Как мы посмотрели ранее, через свойство Handler устанавливается и потом вызывается делегат,
    который обрабатывает запрос по маршруту.

    А свойство RouteData предназначено для хранения информации о маршруте и его значениях. Оно
    представляет объект одноименного класса RouteData, который имеет ряд свойств, содержащих
    информацию об обрабатываемом маршруте:

    Values представляет словарь значений маршрута. Эти значения получены с помощью сегментирования
    строки запроса URL. При обработке мы можем использовать эти значения.

    DataTokens содержит набор дополнительных данных, которые связаны с обрабатываемым маршрутом.
    Однако если данные в RouteData.Values должны быть легко преобразованы в строки и обратно,
    то в RouteData.DataTokens могут быть данные любых типов.

    Routers хранит список маршрутов, которые использовались для успешного сопоставления с запросом. 
    Первый объект в этом списке представляет коллекцию маршрутов, которая применяется для генерации
    URL. А последний элемент этого списка - собственно тот маршрут, который совпал с запросом.

    И все эти данные мы можем получить при обрабтке запроса. Однако делегат RequestDelegate, 
    который обрабатывает запрос в качестве параметра принимает не объект RouteContext, 
    а объект HttpContext. Например:
	
    private async Task Handle(HttpContext context)
    {
        await context.Response.WriteAsync("Hello ASP.NET Core");
    }

    Но у объекта HttpContext есть метод расширения GetRouteData(), который позволяет получить
    объект RouteData:
        RouteData routeData = context.GetRouteData();

    Для получения отдельных данных из словаря RouteData.Values также можно использовать еще
    один метод класса HttpContext - метод context.GetRouteValue()
        string controller = context.GetRouteValue("controller").ToString();
 
*/

namespace Metanit_05_03_WorkingWithRouters
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var myRouteHandler = new RouteHandler(Handle);
            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            routeBuilder.MapRoute("default", "{action=Index}/{name}-{year}");
            routeBuilder.MapRoute("default2", "{controller}/{action}/{id?}");
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private async Task Handle(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var action = routeValues["action"].ToString();
            var name = routeValues["name"].ToString();
            var year = routeValues["year"].ToString();
            await context.Response.WriteAsync($"action: {action} | name: {name} | year:{year}");
        }
    }
}
