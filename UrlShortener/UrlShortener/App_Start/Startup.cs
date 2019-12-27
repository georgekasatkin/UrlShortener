using Microsoft.Owin;
using Owin;
using UrlShortener.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(UrlShortener.Startup))]

namespace UrlShortener
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            app.CreatePerOwinContext<AuthorizationContext>(AuthorizationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}