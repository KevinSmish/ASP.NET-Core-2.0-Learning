using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_03_02_ServiceAndMiddleware
{
    // После добавления сервисов в классе Startup они становятся доступными приложении, в том числе 
    // и в кастомных компонентах middleware. В middleware мы можем получить зависимости тремя способами:
    // Через конструктор
    // Через параметр метода Invoke
    // Через свойство HttpContext.RequestServices

    // При этом надо учитывать, что компоненты middleware создаются при создании класса Startup и живут 
    // в течение всего жизненного цикла приложения.То есть при последующих запросах asp.net core использует
    // уже ранее созданный компонент.И это налагает ограничения на использование зависимостей в middleware.

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TimerMiddleware>();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
