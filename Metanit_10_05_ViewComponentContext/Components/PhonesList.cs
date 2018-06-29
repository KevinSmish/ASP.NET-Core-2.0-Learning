using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_10_05_ViewComponentContext.Components
{
    public class PhonesList : ViewComponent
    {
        Dictionary<string, int> phones;
        public PhonesList()
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

        public IViewComponentResult Invoke()
        {
            int maxPrice = phones.Max(x => x.Value);

            // если передан параметр id
            if (RouteData.Values.ContainsKey("id"))
                Int32.TryParse(RouteData.Values["id"]?.ToString(), out maxPrice);

            ViewBag.Phones = phones.Where(p => p.Value <= maxPrice).ToList();
            ViewData["Header"] = $"Список смартфонов с ценой от {maxPrice.ToString("c")} и меньше";

            return View();
        }
    }
}
