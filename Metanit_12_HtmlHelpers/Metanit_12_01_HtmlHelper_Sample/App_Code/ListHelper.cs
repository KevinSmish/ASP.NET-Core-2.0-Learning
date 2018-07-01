using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_12_01_HtmlHelper_Sample.App_Code
{
    public static class ListHelper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            string result = "<ul>";
            foreach (string item in items)
            {
                result += $"<li>{item}</li>";
            }
            result += "</ul>";
            return new HtmlString(result);
        }
    }
}
