using Metanit_10_01_ViewComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_10_01_ViewComponent.Components
{
    // Вариант 1
    // public class BestPhone : ViewComponent
    // Фактически здесь только было добавлено наследование, а в названии класса 
    // убран суффикс "ViewComponent". Остальной код остался прежним. Применение компонента 
    // в представлении то же самое.

    // Вариант 2
    // [ViewComponent]
    // public class BestPhone
    // ... - далее то же самое

    // Вариант 3 - к названию класса добавляем ...ViewComponent
    public class BestPhoneViewComponent
    {
        List<Phone> phones;
        public BestPhoneViewComponent()
        {
            phones = new List<Phone>
            {
                new Phone {Title="iPhone 7", Price=56000},
                new Phone {Title="Idol S4", Price=26000 },
                new Phone {Title="Elite x3", Price=55000 },
                new Phone {Title="Honor 8", Price=23000 }
            };
        }
        public string Invoke()
        {
            var item = phones.OrderByDescending(x => x.Price).Take(1).FirstOrDefault();

            return $"Самый дорогой телефон: {item.Title}  Цена: {item.Price}";
        }
    }
}
