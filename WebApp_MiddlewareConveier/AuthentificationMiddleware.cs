using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class AuthentificationMiddleware
    {
        RequestDelegate _next;

        public AuthentificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (String.IsNullOrEmpty(token))
                context.Response.StatusCode = 403;
            else
                await _next(context);
        }
    }
}
