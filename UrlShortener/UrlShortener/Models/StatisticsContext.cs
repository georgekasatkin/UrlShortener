using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UrlShortener.Entities;

namespace UrlShortener.Models
{
    public class StatisticsContext : DbContext
    {
        public StatisticsContext() : base("Shortnr") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }
    }
}