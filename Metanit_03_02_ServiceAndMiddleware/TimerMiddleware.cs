using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_03_02_ServiceAndMiddleware
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        TimeService _timeService;

        public TimerMiddleware(RequestDelegate next, TimeService timeService)
        {
            _next = next;

            // В итоге, если мы используем в адресной строке путь "time", то приложение выведет текущее время:
            // Однако сколько бы мы раз не обращались по этому пути, мы все время будем получать одно и то же
            // время, так как объкт TimerMiddleware был создан еще при первом запросе. Поэтому передача через
            // конструктор middleware больше подходит для сервисов с жизненным циклом Singleton, которые 
            // создаются один раз для всех последующих запросов.

            _timeService = timeService;
        }

        /* 
        // ************* v1
        public async Task Invoke(HttpContext context)                       // экземпляр timeService не передаем
        {
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {_timeService?.Time}"); // используем старый
            }
            else
            {
                await _next.Invoke(context);
            }
        }
        */

        /*
        // ************* v2: передаем новый экземпляр класса параметром Invoke
        public async Task Invoke(HttpContext context, TimeService timeService)
        {
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                //await context.Response.WriteAsync($"Текущее время: {_timeService?.Time}");
                await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
        */

        // ************* v3: передаем новый экземпляр класса через context
        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                TimeService timeService = context.RequestServices.GetService<TimeService>();
                await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
            }
            else
            {
                await context.Response.WriteAsync($"Параметр неопределен");
            }
        }
        
    }
}
