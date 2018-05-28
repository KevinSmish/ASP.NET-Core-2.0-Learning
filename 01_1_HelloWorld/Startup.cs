using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _01_1_HelloWorld
{
    public class Startup
    {

        // При запуске приложения сначала срабатывает конструктор, 
        // затем метод ConfigureServices() и в конце метод Configure(). 
        // Эти методы вызываются средой выполнения ASP.NET.

        // Необязательный метод ConfigureServices() регистрирует сервисы, 
        // которые используются приложением. В качестве параметра он принимает объект 
        // IServiceCollection, который и представляет коллекцию сервисов в приложении. 
        // С помощью методов расширений этого объекта производится конфигурация приложения 
        // для использования сервисов. Все методы имеют форму Add[название_сервиса].

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Привет мир!");
            });
        }
    }
}
