using MySql.Data.EntityFramework;
using UrlShortener.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UrlShortener.Models
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ShortnrContext : DbContext
    {
        public ShortnrContext(): base("Shortnr"){ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stat>()
                .HasRequired(s => s.ShortUrl)
                .WithMany(u => u.Stats)
                .Map(m => m.MapKey("shortUrl_id"));
        }
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }
        public virtual DbSet<Stat> Stats { get; set; }
    }

    
}
