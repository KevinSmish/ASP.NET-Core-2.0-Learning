using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

/*
    Версии файлов

    У кэширования есть недостаток: если мы изменим, например, содержимое файла css или javascript, 
    то при повторном обращении к приложению браузер продолжит извлекать нужные файлы из кэша (если 
    срок кэширования не истек). И таким образом, мы будем использовать старые версии файлов.

    Для решения этой проблемы можно добавлять к статическим файлам версию и при каждом изменении 
    файла соответственно менять версию файла:
        <link rel="stylesheet" href="/css/site.css?v=123" />
        <script src="/js/site.js?v=123"></script>
        <img src="/images/banner1.svg?v=123" />

    Если мы используем фреймворк MVC, то для автоматической генерации версии можно воспользоваться 
    встроенными tag-хелперами LinkTagHelper, ScriptTagHelper и ImageTagHelper, которые с помощью 
    атрибута asp-append-version="true" позволяют автоматически добавлять версию при изменении файла:
        <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
        <script src="~/js/site.js" asp-append-version="true"></script>
        <img src="~/images/banner1.svg" asp-append-version="true" />
*/
namespace Metanit_17_03_Compression
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // подключаем компрессию
            app.UseResponseCompression();

            /*
                Кроме кэширования результатов действий контроллера для оптимизации работы приложения 
                можно применять кэширование статического контекта. В данном случае статический контент - 
                это файлы изображений, скрипты javascript, файлы стилей css, какие-то другие файлы, 
                например, аудио-файлы, используемые на странице. Как правило, подобные файлы в проекте 
                размещаются в папке wwwroot.

                Для кэширования статических файлов при их отправке следует установить соответствующие
                заголовки. Для этого StaticFilesMiddleware передается объект StaticFileOptions, у 
                которого устанавливается свойство OnPrepareResponse.
            */
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
                }
            });

            app.Run(async context =>
            {
                // отправляемый текст
                string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." +
                                    "Pri quas audiam virtute ut, case utamur fuisset eam ut, iisque accommodare an eam.Reque blandit qui eu, cu vix nonumy volumus.Legendos intellegam id usu, vide oporteat vix eu, id illud principes has. Nam tempor utamur gubergren no.";

                // установка mime-типа отправляемых данных
                //context.Response.ContentType = "text/plain";
                // отправка ответа
                await context.Response.WriteAsync(loremIpsum);
            });
        }
    }
}
