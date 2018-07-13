using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_18_04_IActionFilter.Filters
{
    public class CheckFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                        ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
                context.ActionArguments["id"] = 34;
            await next();
        }
    }
}
