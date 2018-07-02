using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
    AnchorTagHelper представляет тег-хелпер, который позволяет создавать ссылки. 
    Он может принимать ряд специальных атрибутов:
        asp-controller: указывает на контроллер, которому предназначен запрос
        asp-action: указывает на действие контроллера
        asp-host: указывает на домен сайта
        asp-protocol: определяет протокол (http или https)
        asp-route: указывает на название маршрута
        asp-all-route-data: устанавливает набор значений для параметров
        asp-route-[название параметра]: определяет значение для определенного параметра
        asp-fragment: определяет ту часть хэш-ссылки, которая идет после символа решетки #. 
            Например, "paragraph2" в ссылке "http://mysite.com/#paragraph2"

    *********************************************************************************************
    Для подключения внешних файлов скриптов применяется тег-хэлпер ScriptTagHelper. 
    Тег, представляющий данный класс, может принимать ряд атрибутов:
        asp-append-version: если имеет значение true, то к пути к файлу скрипта добавляется номер версии
        asp-fallback-src: указывает вспомогательный путь к скрипту, который используется, 
            если загрузка скрипта, указанного в атрибуте src пройдет неудачно
        asp-fallback-test: определяет выражение, которое тестирует загрузку основного
            скрипта из атрибута src
        asp-src-include: определяет шаблон подключаемых файлов, через запятую можно задать 
            несколько шаблонов
        asp-src-exclude: определяет через запятую набор шаблонов для тех файлов, 
            которые следует исключить из загрузки
        asp-fallback-src-include: определяет через запятую набор шаблонов файлов, 
            которые подключаются в том случае, если загрузка основного скрипта из
            атрибута src прошла неудачно
        asp-fallback-src-exclude: определяет через запятую набор шаблонов файлов, 
            которые следует исключить из загрузки в том случае, если загрузка основного
            скрипта из атрибута src прошла неудачно
        asp-route-[название параметра]: определяет значение для определенного параметра
 
    *********************************************************************************************
    Например, на мастер-странице _Layout.cshtml в проекте по шаблону Web Application используется 
        следующий тег:

    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-2.2.0.min.js"
        asp-fallback-src="~/lib/jquery/dist/jquery.min.js"
        asp-fallback-test="window.jQuery"
        crossorigin="anonymous"
        integrity="sha384-K+ctZQ+LL8q6tP7I94W+qzQsfRV2a+AfHIi9k8z8l9ggpc8X+Ytst4yBo/hH+8Fk">
    </script>

    Данный элемент представляет не просто стандартный тег script, но и класс тег-хэлпера
    ScriptTagHelper. Атрибут src указывает на скрипт, который мы хотим подключить. Логично
    подключать скрипты из CDN, чтобы сократить нагрузку на собственный сайт. Но CDN может не
    работать, например, произойдет какой-то временный сбой, и чтобы определить, что скрипт
    загружен, применяется атрибут asp-fallback-test. Он тестирует загрузку с помощью выражения
    window.jQuery. Если объект window.jQuery определен, то загрузка скрипта прошла успешно. 
    Если же нет, то загружается скрипт, который указан в атрибуте asp-fallback-src.

    *********************************************************************************************
    LinkTagHelper
    Класс LinkTagHelper определяет тег link, который используется для подключения файлов стилей. 
    Он применяет следующие атрибуты:
        asp-append-version: если имеет значение true, то к пути к названию файла стиля добавляется 
            номер версии
        asp-fallback-href: указывает вспомогательный путь к файлу стиля, который используется, 
            если загрузка файла, указанного в атрибуте href пройдет неудачно
        asp-fallback-test-class: определяет класс, который используется для теста загрузки 
            стиля из атрибута href
        asp-fallback-test-property: определяет свойство, которое используется для тестирования
            загрузки стиля из атрибута href
        asp-fallback-test-value: определяет значение свойства из атрибута asp-fallback-test-property, 
            которое используется для теста загрузки стиля из атрибута href
        asp-href-include: определяет через запятую набор шаблонов подключаемых файлов стилей
        asp-href-exclude: определяет через запятую набор шаблонов для тех файлов, которые 
            следует исключить из загрузки
        asp-fallback-href-include: определяет через запятую набор шаблонов файлов, 
            которые подключаются в том случае, если загрузка основного файла стиля из атрибута href 
            прошла неудачно
        asp-fallback-href-exclude: определяет через запятую набор шаблонов файлов, 
            которые следует исключить из загрузки в том случае, если загрузка основного файла 
            стиля из атрибута href прошла неудачно

    *********************************************************************************************
    Например, на мастер-странице _Layout.cshtml, которая имеется по умолчанию в проекте
    по типу Web Application, есть следующий код:

    <link rel="stylesheet" href="https://ajax.aspnetcdn.com/ajax/bootstrap/3.0.0/css/bootstrap.min.css"
                  asp-fallback-href="~/lib/bootstrap/dist/css/bootstrap.min.css"
                  asp-fallback-test-class="hidden" asp-fallback-test-property="visibility" asp-fallback-test-value="hidden" />

    Здесь атрибут href указывает на файл стилей фреймворка bootstrap, который располагается в CDN. 
    Если веб-браузер не сможет загрузить данный файл, то загружается локальный файл стилей, 
    путь к которому указан в атрибуте asp-fallback-href. Чтобы протестировать, что файл стилей 
    из атрибута href нормально загрузился, используются атрибуты asp-fallback-test-class, 
    asp-fallback-test-property и asp-fallback-test-value.

    В конечном счете этот элемент будет генерировать следующий код, который будет включен 
    на веб-страницу:

    <link rel="stylesheet" href="https://ajax.aspnetcdn.com/ajax/bootstrap/3.0.0/css/bootstrap.min.css" />
        <meta name="x-stylesheet-fallback-test" class="hidden" />
        <script>!function(a,b,c){var d,e=document,f=e.getElementsByTagName("SCRIPT"),g=f[f.length-1].previousElementSibling,h=e.defaultView&&e.defaultView.getComputedStyle?e.defaultView.getComputedStyle(g):g.currentStyle;if(h&&h[a]!==b)for(d=0;d<c.length;d++)e.write('<link rel="stylesheet" href="'+c[d]+'"/>')}("visibility","hidden",["\/lib\/bootstrap\/dist\/css\/bootstrap.min.css"]);</script>

    *********************************************************************************************
    Cache busting

    При работе со статическими файлами, в частности, со стилями css и скриптами js 
    мы можем столкнуться со следующей проблемой. Допустим, у нас есть файл стиля styles.css. 
    Для увеличения производительности подобные статические файлы часто кэшируются на стороне клиента.
    А это значит, что браузеру достаточно один раз за определенный период получить файл и затем 
    при обращении к сайту он будет брать этот файл из кэша. Однако если мы внесем в файл styles.css
    какие-то изменения, то браузер по прежнему будет брать данный файл из кэша и будет использовать
    старые данные, пока не закончится период кэширования.

    Для решения этой проблемы мы можем использовать в ScriptTagHelper и LinkTagHelper параметр
    asp-append-version:
        <ink rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    После обработки запроса будет сгенерирован элемент наподобие следующего:
        <link rel="stylesheet" href="/css/site.css?v=1wp5zz4e-mOPFx4X2O8seW_DmUtePn5xFJk1vB7JKRc">
    К пути к файлу после его имени добавляется параметр ?v=, который указывает на версию файла. 
    Если мы внесем изменения в файл, версия изменится. Соответственно даже если файл и был 
    закэширован ранее в браузере, то смена версии позволит использовать уже новую версию файла.

*/

namespace Metanit_13_01_TagHelper_Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
