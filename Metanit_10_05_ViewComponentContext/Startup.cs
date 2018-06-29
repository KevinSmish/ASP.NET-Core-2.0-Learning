using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

/*
 Если мы создаем компонент с помощью наследования от класса ViewComponent, 
 то нам становится доступным контекст компонента через ряд свойств:

    HttpContext: представляет контекст, который описывает полученный запрос, а также отправляемый ответ

    ModelState: представляет состояние модели в виде объекта ModelStateDictionary

    Request: возвращает контекст запроса в виде объекта HttpRequest

    RouteData: возвращает данные маршрута

    Url: представляет объект IUrlHelper, который используется для генерации адресов URL

    User: представляет текущего пользователя в виде объкта IPrincipal

    ViewBag: представляет динамический объект, который может использоваться для передачи 
        данных в представление

    ViewContext: описывает контекст главного представления, в котором вызывается компонент

    ViewComponentContext: представляет объект ViewComponentContext, который инкапсулирует 
        контекст компонента

    ViewData: возвращает объект ViewDataDictionary, который применяется для передачи данных
        в представление

*/
namespace Metanit_10_05_ViewComponentContext
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // https://localhost:44387/Home/Index/35000

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
