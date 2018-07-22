using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Metanit_21_09_JSON_Web_Token.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

/*
    Общие подходы к авторизации и аутентификации в ASP.NET Core Web API несколько отличаются от того, 
    что мы имеем в MVC. В частности, в Web API механизм авторизации полагается преимущественно на 
    JWT-токены. Что такое JWT-токен?

    JWT (или JSON Web Token) представляет собой веб-стандарт, который определяет способ передачи данных
    о пользователе в формате JSON в зашифрованном виде.

    JWT-токен состоит из трех частей:
        Header - объект JSON, который содержит информацию о типе токена и алгоритме его шифрования
        Payload - объект JSON, который содержит данные, нужные для авторизации пользователя
        Signature - строка для верификации токена, которая создается с помощью секретного кода, Header и Payload. 
*/

namespace Metanit_21_09_JSON_Web_Token
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                       };
                   });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
        }
        /*
            Для установки аутентификации с помощью токенов в методе ConfigureServices 
            в вызов services.AddAuthentication передается значение JwtBearerDefaults.AuthenticationScheme. 
            Далее с помощью метода AddJwtBearer() добавляется конфигурация токена.

            Для конфигурации токена применяется объект JwtBearerOptions, который позволяет с помощью
            свойств настроить работу с токеном. В данном случае использованы следующие свойства:

            RequireHttpsMetadata: если равно false, то SSL при отправке токена не используется. 
            Однако данный вариант установлен только дя тестирования. В реальном приложении все же 
            лучше использовать передачу данных по протоколу https.

            TokenValidationParameters: параметры валидации токена - сложный объект, определяющий, 
            как токен будет валидироваться. Этот объект в свою очередь имеет множество свойств, 
            которые позволяют настроить различные аспекты валидации токена. Но наиболее важные 
            свойства: IssuerSigningKey - ключ безопасности, которым подписывается токен, и 
            ValidateIssuerSigningKey - надо ли валидировать ключ безопасности. Ну и кроме того, 
            можно установить ряд других свойств, таких как нужно ли валидировать издателя и 
            потребителя токена, срок жизни токена, можно установить название claims для ролей и 
            логинов пользователя и т.д.

        */
    }
}
