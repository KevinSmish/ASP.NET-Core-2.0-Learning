using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class RoutingMiddleware
    {
        RequestDelegate _next;
        public RoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();
            if (path == "/" || path == "/index")
                await context.Response.WriteAsync("Home page");
            else if (path == "/about")
                await context.Response.WriteAsync("About");
            else
                context.Response.StatusCode = 404;

        }
    }
}
