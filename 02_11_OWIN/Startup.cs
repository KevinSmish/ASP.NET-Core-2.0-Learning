using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _02_11_OWIN
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOwin(pipeline =>
            {
                pipeline(next => SendResponseAsync);
            });
        }

        public Task SendResponseAsync(IDictionary<string, object> environment)
        {
            // получаем заголовки запроса
            var requestHeaders = (IDictionary<string, string[]>)environment["owin.RequestHeaders"];

            // определяем ответ
            // string responseText = "Hello ASP.NET Core";

            // получаем данные по User-Agent
            string responseText = requestHeaders["User-Agent"][0];

            // кодируем его в массив байтов
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            // получаем поток ответа
            var responseStream = (Stream)environment["owin.ResponseBody"];
            // отправка ответа
            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }

        /*
        Ключи запроса
        owin.RequestBody : объект Stream, представляющий тело запроса
        owin.RequestHeaders : объект IDictionary<string, string[]>, представляющий заголовки запроса
        owin.RequestMethod : объект string, представляющий метод запроса ("GET", "POST")
        owin.RequestPath : объект string, представляет запрошенный путь относительно корня приложения
        owin.RequestPathBase : объект string, содержащий часть запрошенного пути относительно корня приложения
        owin.RequestProtocol : объект string, хранящий название протокола ("HTTP/1.0", "HTTP/1.1")
        owin.RequestQueryString : объект string, хранящий строку запроса, то есть ту часть URI, которая идет после "?" (например, "foo=bar&mes=hello")
        owin.RequestScheme : объект string, хранящий схему URI запроса ("http", "https")
        owin.RequestId : объект string, представляющий уникальный идентификатор запроса (необязательный параметр)

        Ключи ответа
        owin.ResponseBody : объект Stream, представляющий поток ответа
        owin.ResponseHeaders : объект IDictionary<string, string[]>, представляющий заголовки ответа
        owin.ResponseStatusCode : объект int, хранящий статусный код HTTP, который посылается в ответе (необязательный параметр)
        owin.ResponseReasonPhrase : объект string, содержащий словесное пояснение к статусному коду (необязательный параметр)
        owin.ResponseProtocol : объект string, хранящий название протокола ("HTTP/1.0", "HTTP/1.1")
        */
    }
}
