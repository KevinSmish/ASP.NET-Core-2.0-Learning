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

        // Используя различные методы внедрения зависимостей, можно управлять жизненным циклом 
        // создаваемых сервисов. Сервисы, которые создаются механизмом Depedency Injection, 
        //могут представлять один из следующих типов:

        // - Transient: объект сервиса создается каждый раз, когда к нему обращается запрос.
        // Подобная модель жизненного цикла наиболее подходит для легковесных сервисов, 
        // которые не хранят данных о состоянии

        // - Scoped: для каждого запроса создается свой объект сервиса
        // - Singleton: объект сервиса создается при первом обращении к нему, все последующие 
        // запросы используют один и тот же ранее созданный объект сервиса

        // Для создания каждого типа сервиса предназначен соответствующий метод 
        // AddTransient(), AddScoped() и AddSingleton().

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<IMessageSender>(provider => {

                if (DateTime.Now.Hour >= 12) return new EmailMessageSender();
                else return new SmsMessageSender();
            });

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
                IMessageSender messageSender = app.ApplicationServices.GetService<IMessageSender>();
                await context.Response.WriteAsync(messageSender.Send());
                
                //await context.Response.WriteAsync($"Текущее время: {timeService?.GetTime()}");
            });
        }
    }
}
