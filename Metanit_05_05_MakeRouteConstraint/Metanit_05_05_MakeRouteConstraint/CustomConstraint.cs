using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_05_05_MakeRouteConstraint
{
   
    public class CustomConstraint : Microsoft.AspNetCore.Routing.IRouteConstraint
    {
        private string uri;
        public CustomConstraint(string uri)
        {
            this.uri = uri;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, 
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !(uri == httpContext.Request.Path);
        }
    }
}
