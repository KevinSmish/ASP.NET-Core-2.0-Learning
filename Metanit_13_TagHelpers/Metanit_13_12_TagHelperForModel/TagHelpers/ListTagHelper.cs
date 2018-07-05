using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_13_12_TagHelperForModel.TagHelpers
{
    public class ListTagHelper : TagHelper
    {
        public List<string> Elements { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            string listContent = "";
            foreach (string item in Elements)
                listContent += "<li>" + item + "</li>";

            output.Content.SetHtmlContent(listContent);
        }
    }
}
