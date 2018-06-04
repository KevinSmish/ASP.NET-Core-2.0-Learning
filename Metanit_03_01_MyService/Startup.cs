using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Metanit_03_01_MyService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_03_01_MyService
{
    public class Startup
    {
        // С помощью вызова services.AddTransient<IMessageSender, EmailMessageSender>(); 
        // в методе ConfigureServices система на место объектов интерфейса IMessageSender
        // будет передавать экземпляры класса EmailMessageSender. После добавления в ConfigureServices 
        // сервисы можно получить и использовать в любой части приложения. И через параметр метода 
        // Configure мы можем получить сервис и использовать его.

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<TimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env//, IMessageSender messageSender)
                                                                                , TimeService timeService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";

                // Old position
                //IMessageSender messageSender = app.ApplicationServices.GetService<IMessageSender>();
                //await context.Response.WriteAsync(messageSender.Send());
                
                await context.Response.WriteAsync($"Текущее время: {timeService?.GetTime()}");
            });
        }
    }
}
