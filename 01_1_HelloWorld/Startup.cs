using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _01_1_HelloWorld
{
    public class Startup
    {
        private IServiceCollection _services;

        // При запуске приложения сначала срабатывает конструктор, 
        // затем метод ConfigureServices() и в конце метод Configure(). 
        // Эти методы вызываются средой выполнения ASP.NET.

        // *******************************************
        // Конструктор является необязательной частью класса Startup. 
        // В конструкторе, как правило, производится начальная конфигурация приложения.
        // Если мы создаем проект ASP.NET Core по типу Empty, то класс Startup в таком 
        // проекте по умолчанию не содержит конструктор.Но при необходимости мы можем 
        // его определить.

        // Можно создать конструктор без параметров, а можно в качестве параметров 
        // передать любые сервисы, которые добавляются через метод ConfigureServices 
        // или доступны для приложения по умолчанию.
        // *******************************************
        IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }

        // Необязательный метод ConfigureServices() регистрирует сервисы, 
        // которые используются приложением. В качестве параметра он принимает объект 
        // IServiceCollection, который и представляет коллекцию сервисов в приложении. 
        // С помощью методов расширений этого объекта производится конфигурация приложения 
        // для использования сервисов. Все методы имеют форму Add[название_сервиса].

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Метод services.AddMvc() добавляет в коллекцию сервисов сервисы MVC. 
            // После добавления в коллекцию сервисов добавленные севисы становятся 
            // доступными для приложения.
            services.AddMvc();

            _services = services; // сохраним коллекцию сервисов
        }

        
        // Метод Configure устанавливает, как приложение будет обрабатывать запрос. 
        // Этот метод является обязательным. Для установки компонентов, которые обрабатывают 
        // запрос, используются методы объекта IApplicationBuilder. Объект IApplicationBuilder 
        // является обязательным параметром для метода Configure.

        // Кроме того, метод нередко принимает еще два необязательных параметра: 
        // IHostingEnvironment и ILoggerFactory:

        // IHostingEnvironment: позволяет взаимодействовать со средой, 
        // в которой запускается приложение

        // ILoggerFactory: предоставляет механизм логгирования в приложении

        // Но в принципе в метод Configure в качестве параметра может передаваться 
        // любой сервис, который зарегистрирован в методе ConfigureServices или который 
        // регистрируется для приложения по умолчанию (например, IHostingEnvironment 
        // или ILoggerFactory).

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // если приложение в процессе разработки
            if (env.IsDevelopment())
            {
                // то выводим подробную информацию об ошибке, при наличии ошибки
                app.UseDeveloperExceptionPage();
            }

            // обработка запроса - получаем констекст запроса в виде объекта context
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                // отправка ответа в виде строки "Привет, мир!"
                // await context.Response.WriteAsync("Привет мир!");

                await context.Response.WriteAsync(_env.ApplicationName);

                // Выведем список сервисов
                var sb = new StringBuilder();
                sb.Append("<h1>Все сервисы</h1>");
                sb.Append("<table>");
                sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(sb.ToString());
            });
        }
    }
}
