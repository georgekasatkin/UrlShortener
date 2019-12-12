using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UrlShortener.Models
{
    public class Url
    {
        [Required]
        [JsonProperty("longUrl")]
        public string LongURL { get; set; }

        [JsonProperty("shortUrl")]
        public string ShortURL { get; set; }

        [JsonIgnore]
        public string CustomSegment { get; set; }
    }
}