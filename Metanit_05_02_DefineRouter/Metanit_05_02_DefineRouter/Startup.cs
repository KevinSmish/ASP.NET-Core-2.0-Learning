using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_05_02_DefineRouter
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
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapRoute("{controller}/{action}",
                async context => {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("двухсегментный запрос");
                });


            routeBuilder.MapRoute("{controller}/{action}/{id}",
                async context => {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("трехсегментный запрос");
                });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}

/*
    Статические сегменты

    Однако сегменты шаблона необязательно должны представлять параметры URL, 
    это также могут быть константные значения. Например, пусть у нас будет 
    определен следующий маршрут:
	
    routeBuilder.MapRoute("default", "store/{action}");

    Шаблон опять же состоит из двух сегментов, но первый сегмент представляет 
    константное значение "store", поэтому такой маршрут будет соответствовать, 
    например, такому запросу http://localhost:xxxx/Store/Order или любому другому 
    запросу, который состоит из двух сегментов, и первым сегментов обязательно 
    идет "Store", а второй сегмент по-прежнему может представлять любое значение 
    для параметра "action".


*/