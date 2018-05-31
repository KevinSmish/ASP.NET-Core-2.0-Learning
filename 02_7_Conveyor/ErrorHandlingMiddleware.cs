using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_7_Conveyor
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        private string _name;

        public ErrorHandlingMiddleware(RequestDelegate next, string name)
        {
            _next = next;
            _name = name;
        }

        public async Task Invoke(HttpContext context)
        {

            await _next.Invoke(context);

            await context.Response.WriteAsync(" " + _name + ": ");

            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied");
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
