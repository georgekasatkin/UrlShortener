using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;

namespace UrlShortener.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("Shortnr") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}