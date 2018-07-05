using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_13_08_CreateTagHelper.TagHelpers
{
    public class SocialsTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            // получаем вложенный контекст из дочерних tag-хелперов
            var target = await output.GetChildContentAsync();
            var content = "<h3>Заголовок в социальных сетях</h3>" + target.GetContent();
            output.Content.SetHtmlContent(content);
            output.PreElement.SetHtmlContent("<h3>Социальные сети</h3>");
            output.PostElement.SetHtmlContent("<p>Элемент после ссылки</p>");
        }
    }

    // ----------------------------------------------------------------------------------
    public class VKTagHelper : TagHelper
    {
        private const string address = "https://vk.com/metanit";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // заменяет тег <vk> тегом <a>
                                     // присваивает атрибуту href значение из address
            output.Attributes.SetAttribute("href", address);
            output.Content.SetContent("Группа в ВК");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }

    // ----------------------------------------------------------------------------------
    public class VkOneTagHelper : TagHelper
    {
        private const string domain = "https://vk.com/";

        public string Group { get; set; }
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }
            else
            {
                output.TagName = "a";
                output.Attributes.SetAttribute("href", domain + Group);
            }
        }
    }
}
