using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{

    public class ErrorHandlingMiddleware
    {
        RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);   // сначала выполним все последующие запросы

            if (context.Response.StatusCode == 403)
                await context.Response.WriteAsync("Access Denied");
            else if (context.Response.StatusCode == 404)
                await context.Response.WriteAsync("Page is not found");
        }
    }
}
