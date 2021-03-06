﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_04_01_IConfiguration
{
    // Microsoft.Extensions.Configuration (предоставляет базовую функциональность для конфигурации, а также позволяет задавать параметры конфигурации в памяти)
    // Microsoft.Extensions.Configuration.Json (позволяет использовать в качестве источников конфигурации файлы json)
    // Microsoft.Extensions.Configuration.CommandLine (позволяет считывать параметры конфигурации из командной строки с помощью метода AddCommandLine())
    // Microsoft.Extensions.Configuration.EnvironmentVariables (позволяет получать параметры конфигурации из переменных окружения)
    // Microsoft.Extensions.Configuration.Ini (использует в качестве источника конфигурации файлы ini)
    // Microsoft.Extensions.Configuration.Xml (использует в качестве источника конфигурации файлы xml)

    public class Startup
    {
        // свойство, которое будет хранить конфигурацию
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // строитель конфигурации
            /*
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"color", "blue"},
                    {"text", "Hello ASP.NET 5"}
                });
            */

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("myconfig.json");

            // создаем конфигурацию
            AppConfiguration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //AppConfiguration["text"] = "Hello ASP.NET Core 2.1";

            var color = AppConfiguration["color"];
            //var text = AppConfiguration["text"];
            string text = AppConfiguration["namespace:class:text"];

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"<p style='color:{color};'>{text}</p>");
                //await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
