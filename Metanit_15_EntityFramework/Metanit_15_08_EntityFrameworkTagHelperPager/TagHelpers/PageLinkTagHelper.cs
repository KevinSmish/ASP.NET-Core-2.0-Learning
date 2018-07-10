using Metanit_15_08_EntityFrameworkTagHelperPager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    Фреймворк MVC предоставляет ряд сервисов, и один из них - IUrlHelperFactory, 
    который используется для создания ссылки и который мы можем получить в конструкторе.

    Всю информацию о пагинации мы получаем через свойство PageModel. 
    Свойство PageAction указывает на метод контроллера, на который будет создаваться ссылка.

    Для создания ссылки используется объект IUrlHelper, а для его получения нам нужен 
    контекст представления, в котором вызывается tag-хелпер. Получить контекст представления
    мы можем через внедрение зависимости через атрибуты. В частности, чтобы получить контекст
    представления над свойством ставится атрибут ViewContext:

    Чтобы избежать привязки к атрибутам тега, к свойству также применяется 
    атрибут HtmlAttributeNotBound.
*/

namespace Metanit_15_08_EntityFrameworkTagHelperPager.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound] 
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            // формируем три ссылки - на текущую, предыдущую и следующую
            TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

            // создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }

            tag.InnerHtml.AppendHtml(currentItem);
            // создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
