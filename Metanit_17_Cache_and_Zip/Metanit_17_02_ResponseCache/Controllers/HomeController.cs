using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Metanit_17_02_ResponseCache.Models;

/*
    Атрибут ResponseCache представляет еще один инструмент кэширования. 
    Он предполагает установку в ответе клиенту заголовков кэширования, которые определяют, 
    как клиент и промежуточные прокси-серверы должны кэшировать ответ на своей стороне. 
    При этом ResponseCache не выполняет кэширование на сервере! Его суть - только в установке 
    отправляемых заголовков.

    Заголовок кэширования - это обычные заголовки, которые определяются спецификацией протокола 
    HTTP и которые позволяют управлять кэшированием. То есть это заголовки Cache-Control, Pragma и Vary.

    Основным из них является заголовок Cache-Control, который может принимать следующие значения:
        public: ответ будет кэшироваться везде - и на машине клиента, и на промежуточных прокси-серверах
        private: ответ будет кэшироваться только на компьютере клиента, но промежуточные прокси-серверы
            не будут выполнять кэширование
        no-cache: ответ нигде не будет кэшироваться
        max-age: время кэширования

    Для настройки заголовоков ответа атрибут ResponseCacheAttribute определяет следующие свойства:
        Duration: устанавливает максимальное время кэширования в секундах. Является обязательным, 
            если свойство NoStore не равно true. Duration добавляет к заголовку Cache-Control 
            значение max-age с устанавливаемым временным промежутком в секундах
        Location: определяет место кэширования. 
            Принимает одно из значений из перечисления ResponseCacheLocation:
                Any: ответ кэшируется везде (в том числе и на прокси-серверах). 
                    Является значением по умолчанию
                Client: ответ кэшируется только на компьютере клиента
                None: ответ нигде не кэшируется
                NoStore: определяет, будет ли ответ кэшироваться. Если равен true, 
                    то ответ не кэшируется, а значения свойств Location и Duration игнорируются. 
                    Кроме того, заголовку Cache-Control добавляется значение no-store
                VaryByHeader: устанавливает заголовок vary
                CacheProfileName: определяет имя профиля кэширования
                Order: устанавливает порядок данного атрибута при применении к методу контроллера 
                    одновременно нескольких атрибутов
*/
namespace Metanit_17_02_ResponseCache.Controllers
{
    public class HomeController : Controller
    {
        //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)] // отключим любое кэширование
        // Как видно, браузер клиента получил установленные заголовки:
        // Cache-Control: no-store,no-cache
        // Pragma: no-cache

        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        // Теперь будет выполняться кэширование, так как я явным образом использовал значение 
        // Location =ResponseCacheLocation.Any (оно применяется по умолчанию, поэтому его можно опускать). 
        // Кроме того, срок жизни кэша будет составлять 300 секунд, то есть 5 минут.
        // Перезапустим приложение, и теперь заголовок Cache-Control будет иметь другое значение:
        // Cache-Control: public,max-age=300

        [ResponseCache(CacheProfileName = "NoCaching")]
        // в атрибуте ResponseCache нам надо указать имя нужного профиля через параметр CacheProfileName
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
