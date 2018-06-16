using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace Metanit_05_04_RouteConstraint
{
    /*
        AlphaRouteConstraint: параметр должен состоять только из алфавитных символов английского алфавита
            new {controller=new AlphaRouteConstraint()}

        BoolRouteConstraint: параметр должен иметь значение true или false
            new {id=new BoolRouteConstraint()}

        DateTimeRouteConstraint: параметр должен предоставлять объект DateTime
            new {id=new DateTimeRouteConstraint()}
    
        DecimalRouteConstraint: параметр должен предоставлять объект decimal
            new {id=new DecimalRouteConstraint()}

        DoubleRouteConstraint: параметр должен предоставлять объект double
            new {id=new DoubleRouteConstraint()}

        FloatRouteConstraint: параметр должен предоставлять объект float
            new {id=new FloatRouteConstraint()}

        GuidRouteConstraint: параметр должен предоставлять объект Guid
            new {id=new GuidRouteConstraint()}

        HttpMethodRouteConstraint: запрос должен представлять определенный тип - Get, Post и т.д.
        routeBuilder.MapRoute("default",
             "{controller}/{action}/{id?}",
             null,
             new { httpMethod = new HttpMethodRouteConstraint("POST") }
        );

        В конструктор HttpMethodRouteConstraint передаются названия допустимых типов запросов. 
        В данном случае маршрут будет обрабатываться только для POST-запросов.

        IntRouteConstraint: параметр должен предоставлять объект int
            new {id=new IntRouteConstraint()}

        LengthRouteConstraint: строка, представляющая параметр, должна иметь определенную длину
            new
            {
                controller = new LengthRouteConstraint(4), // точная длина
                action = new LengthRouteConstraint(3,10) // минимальная и максимальная длина
            }

        LongRouteConstraint: параметр должен предоставлять объект long
            new {id=new LongRouteConstraint()}

        MaxLengthRouteConstraint / MinLengthRouteConstraint: определяют максимальную и минимальную длину параметра в символах
        new
        {
            controller = new MaxLengthRouteConstraint(5),
            action = new MinLengthRouteConstraint(3)
        }

        MaxRouteConstraint / MinRouteConstraint: определяют максимальное и минимальное числовое значение для параметра
            new {id=new MinRouteConstraint(4)} // минимальное значение - 4

        RangeRouteConstraint: параметр должен предоставлять числовое значение int в определенном диапазоне
            new {id=new RangeRouteConstraint(3, 100)} // диапазон от 3 до 100

        RegexRouteConstraint: задает регулярное значение, которому должен соответствовать параметр

        OptionalRouteConstraint: определяет ограничение для необязательного параметра. И если параметр имется в запросе, то вступает в силу внутреннее ограничение, задаваемое через свойство InnerConstraint класса OptionalRouteConstraint

        RequiredRouteConstraint: указывает, что параметр должен обязательно иметь какое-нибудь значение

        Составные ограничения
        С помощью специального класса CompositeRouteConstraint можно установить сложное ограничение, 
        которое будет включать несколько простых:
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller="Home", action = "Index" },
                constraints: new
                {
                    action =  new CompositeRouteConstraint(new IRouteConstraint[] 
                    {
                        new AlphaRouteConstraint(),
                        new MinLengthRouteConstraint(6)
                    })
                });

        Строчный синтаксис ограничений
        ASP.NET Core позволяет также указывать ограничения прямо при определении параметров 
        поле двоеточия в виде сокращений, это так называемые строчные ограничения (inline constraints):

        routeBuilder.MapRoute("default", "{controller:regex(^H.*)}/{action}/{id?}");
        Ограничение regex фактически применяет класс RegexRouteConstraint. 
        Все подобные встраиваемые ограничения называются по имени классов без суффикса RouteConstraint.

        Все строчные ограничения:
        int
            Соответствие числу
            {id:int}

        bool
            Соответствие значению true или false
            {active:bool}

        datetime
            Соответствие дате и времени
            {date:datetime}

        decimal
            Соответствие значению decimal
            {price:decimal}

        double
            Соответствие значению типа double
            {weight:double}

        float
            Соответствие значению типа float
            {height:float}

        guid
            Соответствие значению типа Guid
            {id:guid}

        long
            Соответствие значению типа long
            {id:long}

        minlength(value)
            Строка должна иметь символов не меньше value
            {name:minlength(3)}

        maxlength(value)
            Строка должна иметь символов не больше value
            {name:maxlength(20)}

        length(value)
            Строка должна иметь ровно столько символов, сколько определено в параметре value
            {name:length(10)}

        length(min, max)
            Строка должна иметь символов не меньше min и не больше max
            {name:length(3, 20)}

        min(value)
            Число должно быть не меньше value
            {age:min(3)}

        max(value)
            Число должно быть не больше value
            {age:max(20)}

        range(min, max)
            Число должно быть не меньше min и не больше max
            {age:range(18, 99)}

        alpha
            Строка должна состоять из одно и более алфавитных символов
            {name:alpha}

        regex(expression)
            Строка должна соответствовать регулярному выражению expression
            {phone:regex(^\d{{3}}-\d{{3}}-\d{{4}}$)}

        required
            Параметр является обязательным, и его значение должно быть определено
            {name:required}

        Например, установим ряд ограничений:
            routeBuilder.MapRoute("default", "{controller:length(4)}/{action:alpha}/{id:range(4,100)}");

        Если нам надо установить составное ограничение, то все ограничения перечисляются через двоеточие:
        routeBuilder.MapRoute(
            name: "default",
            template: "{controller}/{action:alpha:minlength(6)}/{id?}",
            defaults: new { controller="Home", action = "Index" });

    */

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var myRouteHandler = new RouteHandler(Handle);
            var routeBuilder = new RouteBuilder(app, myRouteHandler);

            routeBuilder.MapRoute("default",
                "{controller}/{action}/{id?}",
                null,
                new { controller = new RegexRouteConstraint("^H"),
                        id = new RegexRouteConstraint(@"\d{2}")
                    }
                );

            // имя начинается с H, id состоит из 2 цифр


            app.UseRouter(routeBuilder.Build());

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
        
        // localhost:53623/Home/Index/01
        private async Task Handle(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var controller = routeValues["controller"].ToString(); 
            var action = routeValues["action"].ToString();
            var id = routeValues["id"].ToString();
            await context.Response.WriteAsync($"controller: {controller} | action: {action} | id: {id}");
        }

    }
}
