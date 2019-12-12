using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UrlShortener.Entities
{
    [Table("short_urls")]
    public class ShortUrl
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("long_url")]
        [StringLength(1000)]
        public string LongUrl { get; set; }

        [Required]
        [Column("segment")]
        [StringLength(20)]
        public string Segment { get; set; }

        [Required]
        [Column("added")]
        public DateTime Added { get; set; }

        [Required]
        [Column("ip")]
        [StringLength(50)]
        public string Ip { get; set; }

        [Required]
        [Column("num_of_clicks")]
        public int NumOfClicks { get; set; }

        [Required]
        [Column("UserName")]
        [StringLength(128)]
        public string UserName { get; set; }

        [Required]
        [Column("customized")]
        public bool Customized { get; set; }

        public Stat[] Stats { get; set; }
    }
    
}
