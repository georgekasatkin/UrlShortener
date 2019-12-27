using Microsoft.AspNet.Identity.EntityFramework;

namespace UrlShortener.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
        public ApplicationUser()
        {
        }
    }
}