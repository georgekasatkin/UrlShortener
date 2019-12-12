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
                name: "StatIndex",
                url: "Statistics/Index/{id}",
                defaults: new { controller = "Statistics", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StatDelete",
                url: "Statistics/Delete/{id}",
                defaults: new { controller = "Statistics", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UrlIndex",
                url: "Url/Index/{id}",
                defaults: new { controller = "Url", action = "Index", id = UrlParameter.Optional }
                , constraints: new { controller = "Url", action = "Index" }
            );
            routes.MapRoute(
                name: "AccLogin",
                url: "Account/Login/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AccLogout",
                url: "Account/Logout/{id}",
                defaults: new { controller = "Account", action = "Logout", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AccRegister",
                url: "Account/Register/{id}",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AccEdit",
                url: "Account/Edit/{id}",
                defaults: new { controller = "Account", action = "Edit", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AccDelete",
                url: "Account/Delete/{id}",
                defaults: new { controller = "Account", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Statistics", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
