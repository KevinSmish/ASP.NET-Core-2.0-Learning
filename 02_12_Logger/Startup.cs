using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Поскольку вывод информации будет происходить на консоль, то для тестирования логгера 
// нам надо запустить приложение как консольное:

/*
Конфигурация логгера

При настройке логгирования мы можем установить уровень детализации информации с помощью одного из значений перечисления LogLevel. Всего мы можем использовать следующие значения:
    Trace: используется для вывода наиболее детализированных сообщений. Подобные сообщения могут нести важную информацию о приложении и его строении, поэтому данный уровень лучше использовать при разработке, но никак не при публикации
    Debug: для вывода информации, которая может быть полезной в процессе разработки и отладки приложения
    Information: уровень сообщений, позволяющий просто отследить поток выполнения приложения
    Warning: используется для вывода сообщений о неожиданных событиях, например, ошибках, которые не влияют не останавливают выполнение приложения, но в то же время должны быть иследованы
    Error: информация об ошибках, вследствие которых приложение должно быть остановлено
    Critical: уровень критических ошибок, которые могут быть связаны с какими-то ситуациями извне - ошибками операционной системы, потерей данных в бд, переполнение памяти диска и т.д.
    None: вывод информации в лог не применяется
*/

namespace _02_12_Logger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            // Старый вариант
            //loggerFactory.AddConsole(LogLevel.Warning);

            app.Run(async (context) =>
            {
                // Старый вариант
                // создаем объект логгера
                // var logger = loggerFactory.CreateLogger("RequestInfoLogger");
                // пишем на консоль информацию
                // logger.LogInformation("Processing request {0}", context.Request.Path);

                // Новый вариант
                logger.LogInformation("Processing request {0}", context.Request.Path);

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
