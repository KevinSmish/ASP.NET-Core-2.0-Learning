using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Metanit_10_02_DependencyInjection.Models
{
    public class BestPhone : ViewComponent
    {
        Dictionary<string, int> phones;
        public BestPhone()
        {
            phones = new Dictionary<string, int>
            {
                {"iPhone 7", 56000 },
                {"Alcatel Idol S4", 26000 },
                {"Samsung Galaxy S7", 50000 },
                {"HP Elite x3", 56000 },
                {"Xiaomi Mi5S", 22000 },
                {"Meizu Pro 6", 22000 },
                {"Huawei Honor 8", 23000 },
                {"Google Pixel", 30000 }
            };
        }
        /*
        public IViewComponentResult Invoke()
        {
            var item = phones.OrderByDescending(p => p.Value).Take(1).FirstOrDefault();
            return Content($"Самый дорогой телефон: {item.Key} - {item.Value.ToString("c")}");
        }
        */

        public IViewComponentResult Invoke()
        {
            var item = phones.OrderByDescending(p => p.Value).Take(1).FirstOrDefault();
            return new HtmlContentViewComponentResult(
                new HtmlString($"<h3>Самый дорогой телефон: {item.Key} - {item.Value.ToString("c")}</h3>"));
        }
    }
}
