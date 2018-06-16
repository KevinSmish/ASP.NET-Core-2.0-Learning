using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_05_06_MakingOurselvesRouters
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
            routeBuilder.Routes.Add(new AdminRoute());
            routeBuilder.MapRoute("{controller}/{action}",
                async context => {
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync("двухсегментный запрос");
                });
            app.UseRouter(routeBuilder.Build());
            // С помощью вызова routeBuilder.Routes.Add(new AdminRoute()) маршрут добавляется 
            // на первое место в списке маршрутов. И в случае, если в запросе первый сегмент 
            // будет начинаться с "Admin", то он и будет соответствовать этому маршруту.
            // http://localhost:61635/admin/1

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
