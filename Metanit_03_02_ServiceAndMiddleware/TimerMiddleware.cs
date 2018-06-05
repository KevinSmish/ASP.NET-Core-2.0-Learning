using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_03_02_ServiceAndMiddleware
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        TimeService _timeService;

        public TimerMiddleware(RequestDelegate next, TimeService timeService)
        {
            _next = next;
            _timeService = timeService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {_timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
