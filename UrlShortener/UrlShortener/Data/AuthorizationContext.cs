using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using UrlShortener.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UrlShortener.Models
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AuthorizationContext : IdentityDbContext<ApplicationUser>
    {
        public AuthorizationContext() : base("Shortnr") { }

        public static AuthorizationContext Create()
        {
            return new AuthorizationContext();
        }
    }
}