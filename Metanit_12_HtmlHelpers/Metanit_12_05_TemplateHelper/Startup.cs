using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_12_05_TemplateHelper
{
    /*
    Кроме html-хелперов форм, которые создают различные элементы формы, фреймворк ASP.NET MVC Core 
    также имеет шаблонные (или шаблонизированные) хелперы. В отличие от рассмотренных в прошлой главе
    html-хелперов они не генерируют определенный элемент html. Шаблонные хелперы смотрят на свойство
    модели и генерируют тот элемент html, который наиболее подходит данному свойству, исходя из его
    типа и метаданных.

    В ASP.NET MVC имеются следующие шаблонные хелперы:
    Display         - Создает элемент разметки для отображения значения указанного свойства модели: Html.Display("Name")
    DisplayFor      - Строго типизированный аналог хелпера Display: Html.DisplayFor(m => m.Name)
    Editor          - Создает элемент разметки для редактирования указанного свойства модели: Html.Editor("Name")
    EditorFor       - Строго типизированный аналог хелпера Editor: Html.EditorFor(m => m.Name)
    DisplayText     - Создает выражение для указанного свойства модели в виде простой строки: Html.DisplayText("Name")
    DisplayTextFor  - Строго типизированный аналог хелпера DisplayText: Html.DisplayTextFor(m => m.Name)

    Это были одиночные хелперы, которые генерируют разметку только для одного свойства модели. 
    Но кроме них во фреймворке также есть еще несколько шаблонов, которые позволяют создать разом
    все поля для всех свойств модели:

    DisplayForModel     - Создает поля для чтения для всех свойств модели: Html.DisplayForModel()
    DisplayTextForModel - Создает поля для чтения для всех свойств модели в виде строки
    EditorForModel      - Создает поля для редактирования для всех свойств модели: Html.EditorForModel()

    *********************************************************
    Html.ActionLink     - создает гиперссылку на действие контроллера. Если мы создаем ссылку 
        на действие, определенное в том же контроллере, то можем просто указать имя действия:
        @Html.ActionLink("О сайте", "About")
            Что создаст вам следующую разметку:
        <a href="/Home/About">О сайте</a>

        Если необходимо указать ссылку на действие из другого контроллера, то в хелпере ActionLink
        в качестве третьего аргумента имя контроллера. 
        Например, ссылка на действие List контроллера Book будет создаваться так:
            @Html.ActionLink("Список книг", "List", "Book")
      
        Кроме того, если у нас в некотором методе Index контроллера BookController
            определено несколько параметров:
        public class BookController : Controller
        {
            public string Index(string author="Толстой", int id=1)
            {
                return author + "  " + id.ToString();
            }
        }
        То перегруженная версия хелпера ActionLink позволяет передать параметр объекта 
        (обычно анонимный тип) для параметра routeValues. Среда выполнения принимает свойства 
        объекта и использует их для создания значений маршрутизации (имена свойств становятся 
        именами параметров маршрута, а значения свойств представляют значения параметра маршрута).
        
        Создадим ссылку для вышеопределенного действия контроллера:
            @Html.ActionLink("Все книги", "Index", "Book", new { id=10}, null)
            //или
            @Html.ActionLink("Достоевский", "Index", "Book", new { author="Достоевский", id=5}, null)

        Теперь попробуем передать атрибуты, например, установить атрибуты id и class:
        @Html.ActionLink("Все книги", "Index", "Book", new {  author="Толстой",id = 10 }, 
            new { id="Tolstoi", @class="link"})

        Сгенерированная html-разметка будет выглядеть следующим образом:
        <a class="link" href="/Book/Index/10?author=%D0%A2%D0%BE%D0%BB%D1%81%D1%82%D0%BE%D0%B9" id="Tolstoi">Все книги</a>

        Обратите внимание на знак @ перед словом class: поскольку слово "class" является 
        зарезервированным словом в C#, то для правильного рендеринга нам надо перед ним указать знак @.

        Html.RouteLink

        *********************************************************************
        Хелпер RouteLink использует похожий шаблон, что и ActionLink: он принимает имя маршрута, 
        но не требует аргументов для имени контроллера и имени действия. Так, первый пример с 
        ActionLink эквивалентен следующему коду:
        @Html.RouteLink("Все книги", new { controller = "Book", action = "Index", author = "Толстой", 
            id = 10 }, new { id = "Tolstoi", @class = "link" })

        Чтобы использовать маршрут, нам просто надо указать имя определенного нами маршрута и затем определить при необходимости дополнительные параметры. Например, возьмем стандартный маршрут default, который определен в классе Startup:
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        Тогда создать ссылку мы можем, например, так:
            @Html.RouteLink("Все книги","Default",new { action = "Show" })

    */

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Details" }
                    );
            });
        }
    }
}
