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
    Для управления выводом хелпера используется объект TagHelperOutput, который передается в качестве
        параметра в метод Process tag-хелпера. Его свойства позволяют управлять генерацией элемента html:
    TagName: указывает, какой элемент html будет создаваться вместо тега хелпера
    TagMode: устанавливает формат создаваемого элемента (с одним или с двумя тегами)
    Attributes: представляет коллекцию атрибутов, устанавливаемых у создаваемого элемента html
    Content: представляет содержимое генерируемого элемента html в виде объекта TagHelperContent
    PreContent: представляет содержимое, которое устанавливается перед создаваемым элементом html
    PostContent: представляет содержимое, которое устанавливается после создаваемого элемента html
    PreElement: представляет html-элемент, который добавляется перед создаваемым элементом html
    PostElement: представляет html-элемент, который добавляется после создаваемого элемента html

    Закрытие элемента
    Элементы html могут состоять из двух тегов (открывающего и закрывающего), либо из одного тега 
        (открывающегося или самозакрывающегося). С помощью свойства TagMode мы можем регулировать
        закрытие элемента. Оно принимает одно из значений перечисления TagMode:
    StartTagAndEndTag: элемент имеет оба тега
    SelfClosing: элемент содержит самозакрывающийся тег
    StartTagOnly: элемент имеет только открывающий тег

    По умолчанию при создании элемента применяется тот же режим закрытия тега, который использовался
        при его использовании. Например, если мы не устанавливаем текст внутри tag-хелпера, 
        то нет смысла определять для него оба тега, и мы можем использовать только один тег:
    <vk />
        Но в этом случае среда будет также создавать ссылку с одним тегом, типа 
        <a href="https://vk.com">. Открытая ссылка приведет к тому, что последующие элементы
        также могут включаться в содержимое ссылки. И чтобы этого избежать, установим для ссылки 
        также и закрывающий тег.

    Управление контентом
    Для управления контентом применяется свойство Content, представляющее объект TagHelperContent, 
    у которого можно выделить следующие методы:

    SetContent(text): устанавливает текстовое содержимое элемента
    SetHtmlContent(html): устанавливает вложенный html-код элемента
    Append(text): добавляет к текстовому содержимому элемента некоторый текст
    AppendHtml(html): добавляет к внутреннему коду элемента некоторый код html
    Clear(): очищает элемент

    Так, выше уже использовался метод output.Content.SetContent("Группа в ВК").
    С помощью дополнительных свойств PreElement/PostElement/PreContent/PostContent, 
        который также представляют объект TagHelperContent, можно управлять контентом вокруг элемента.

*/
namespace Metanit_13_08_CreateTagHelper
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
