using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace _01_1_HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // Чтобы запустить приложение ASP.NET Core, необходим объект IWebHost, 
        // в рамках которого развертывается веб-приложение. Для создания этого объекта 
        // в файле определен вспомогательный метод BuildWebHost().

        // Данный метод выполняет ряд задач. В частности, настраивает веб-сервер Kestrel, 
        // который используется для развертывания приложения, устанавливает корневой каталог 
        // (для этого используется свойство Directory.GetCurrentDirectory). 
        // Корневой каталог представляет папку, где будет производиться поиск различного 
        // содержимого, например, представлений. Также метод настраивает опции конфигурации 
        // и логгирования. А если для работы приложения требуется IIS, то данный метод также 
        // обеспечивает интеграцию с IIS.

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
