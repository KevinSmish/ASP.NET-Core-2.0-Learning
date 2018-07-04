using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

/*
    Тег-хелперы, используемые для создания форм, аналогичны соответствующим элементам html
    за тем исключением, что они добавляют дополнительную функциональность. Так, для создания 
    формы используется класс FormTagHelper, представленный тегом form. Этот тег может принимать 
    следующие атрибуты:
        asp-controller: указывает на контроллер, которому предназначен запрос
        asp-action: указывает на действие контроллера
        asp-area: указывает на название области, в которой будет вызываться контроллер для обработки формы
        asp-antiforgery: если имеет значение true, то для этой формы будет генерироваться 
            antiforgery token
        asp-route: указывает на название маршрута
        asp-all-route-data: устанавливает набор значений для параметров
        asp-route-[название параметра]: определяет значение для определенного параметра

        Например, форма:
            <form asp-antiforgery="true" asp-action="Create" asp-controller="Home">

        В данном случае форма будет отправлять данные методу Create котроллера Home и для формы 
        будет генерироваться antiforgery token.

        Все остальные теги, которые используются на формах, имеют два общих атрибута:
            asp-for: указывает, для какого свойства модели создается элемент
            asp-format: устанавливает формат ввода для элемента

    LabelTagHelper использует тег label для создания метки:
        <label asp-for="Name"></label>
    InputTagHelper создает поле ввода:
        <input asp-for="Name" />

    Кроме атрибута asp-for тег также может принимать атрибут asp-format, который может
    оказаться полезным в некоторых ситуациях. Например, нам надо выводить дату в некотором 
    определенном формате:
        <input asp-for="ReleaseDate" asp-format="{0:dd-MM-yyyy}" />

    TextAreaTagHelper используется для создания многострочного текстового поля textarea. 
    Данный хелпер применяет только атрибут asp-for:
        <textarea asp-for="Name"></textarea>

    SelectTagHelper создает элемент списка:
        <select asp-for="CompanyId" asp-items="ViewBag.Companies"></select>

    Атрибут asp-items указывает на объект IEnumerable<SelectListItem>, который будет 
    использоваться для наполнения списка. При необходимости мы можем указать элемент, 
    который будет отображаться по умолчанию:
        <select asp-for="CompanyId" asp-items="ViewBag.Companies">
            <option selected="selected" disabled="disabled">Выберите компанию</option>
        </select>

*/
namespace Metanit_13_04_TagHelperOfForm
{
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
                routes.MapRoute(name: "default", template: "{controller=Home}/{Action=Index}/{Id?}");
            });
        }
    }
}
