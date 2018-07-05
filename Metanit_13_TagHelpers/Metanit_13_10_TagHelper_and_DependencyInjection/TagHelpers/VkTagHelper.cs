using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_13_10_TagHelper_and_DependencyInjection.TagHelpers
{
    public class VkTagHelper : TagHelper
    {
        private const string domain = "https://vk.com/id";
        IHostingEnvironment environment;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        // получаем сервис IHostingEnvironment
        public VkTagHelper(IHostingEnvironment env)
        {
            environment = env;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // получаем из параметров маршрута id
            string id = ViewContext?.RouteData?.Values["id"]?.ToString();
            if (String.IsNullOrEmpty(id)) id = "1";
            output.TagName = "a";
            output.Attributes.SetAttribute("href", domain + id);
            if (environment.IsDevelopment())
                output.Attributes.SetAttribute("style", "color:red;");
        }
    }
}
