using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

namespace UrlShortener
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Click",
               url: "{segment}",
               defaults: new { controller = "Url", action = "Click" }
           );

            routes.MapRoute(
                name: "UrlIndex",
                url: "Url/Index/{id}",
                defaults: new { controller = "Url", action = "Index", id = UrlParameter.Optional }
                , constraints: new { controller = "Url", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Statistics", action = "Index", id = UrlParameter.Optional }
            );            
        }
    }
}
