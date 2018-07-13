using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_18_05_IResultFilter.Filters
{
    public class TimeExecutionFilterAttribute : Attribute, IResultFilter
    {
        DateTime start;
        public void OnResultExecuting(ResultExecutingContext context)
        {
            start = DateTime.Now;
        }
        public async void OnResultExecuted(ResultExecutedContext context)
        {
            DateTime end = DateTime.Now;
            double processTime = end.Subtract(start).TotalMilliseconds;
            await context.HttpContext.Response.WriteAsync($"Время выполнения результата: {processTime} миллисекунд");
        }

        /*
            При реализации интерфейса IAsyncResultFilter его единственный метод OnResultExecutionAsync 
            объединяет возможности методов OnResultExecuting и OnResultExecuted. Вызов await next() 
            для объекта ResultExecutionDelegate позволит выполнить последующие фильтры результатов 
            или фильтры действий. Чтобы предотвратить дальнейшее выполнение фильтров в методе 
            OnResultExecutionAsync() необходимо установить свойство ResultExecutingContext.Cancel 
            равным true и не вызывать делегат ResultExectionDelegate.    

                public async Task OnResultExecutionAsync(ResultExecutingContext context, 
                                                        ResultExecutionDelegate next)
                {
                    context.HttpContext.Response.Headers.Add("DateTime", DateTime.Now.ToString());
                    await next();
                }
        */
    }
}
