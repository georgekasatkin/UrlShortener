using UrlShortener.Data;
using UrlShortener.Entities;
using UrlShortener.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.UI;

namespace UrlShortener.Models
{
    public class UrlManager : IUrlManager
    {
        public static string GetUserName()
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var name = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            return name ?? "AnonimousUser";
        }

        public Task<ShortUrl> ShortenUrl(string longUrl, string ip, string segment = "")
        {
            return Task.Run(() =>
            {
                using (var ctx = new ShortnrContext())
                {
                    ShortUrl url;
                    bool customized = false;
                    if (!String.IsNullOrEmpty(segment))
                        customized = true;

                    url = ctx.ShortUrls.Where(u => u.Segment == segment).FirstOrDefault();
                    if (url != null)
                    {
                        throw new ArgumentException("Custom link already exists");
                    }

                    url = ctx.ShortUrls.Where(u => u.LongUrl == longUrl && u.Customized == false).FirstOrDefault();
                    if (url != null && customized == false)
                    {
                        return url;
                    }

                    if (!longUrl.StartsWith("http://") && !longUrl.StartsWith("https://"))
                    {
                        throw new ArgumentException("Invalid URL format");
                    }
                    Uri urlCheck = new Uri(longUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
                    request.Timeout = 10000;
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (Exception)
                    {
                        throw new ShortnrNotFoundException();
                        throw;
                    }

                    int cap = 0;
                    string capString = ConfigurationManager.AppSettings["MaxNumberShortUrlsPerHour"];
                    int.TryParse(capString, out cap);
                    DateTime dateToCheck = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
                    int count = ctx.ShortUrls.Where(u => u.Ip == ip && u.Added >= dateToCheck).Count();
                    if (cap != 0 && count > cap)
                    {
                        throw new ArgumentException("Your hourly limit has exceeded");
                    }

                    if (!string.IsNullOrEmpty(segment))
                    {
                        if (ctx.ShortUrls.Where(u => u.Segment == segment).Any())
                        {
                            throw new ShortnrConflictException();
                        }
                        if (segment.Length > 20 || !Regex.IsMatch(segment, @"^[A-Za-z\d_-]+$"))
                        {
                            throw new ArgumentException("Malformed or too long segment");
                        }
                    }
                    else
                    {
                        segment = this.NewSegment();
                    }

                    if (string.IsNullOrEmpty(segment))
                    {
                        throw new ArgumentException("Segment is empty");
                    }

                    url = new ShortUrl()
                    {
                        Added = DateTime.Now,
                        Ip = ip,
                        LongUrl = longUrl,
                        NumOfClicks = 0,
                        Segment = segment,
                        UserName = GetUserName(),
                        Customized = customized
                    };

                    ctx.ShortUrls.Add(url);

                    ctx.SaveChanges();

                    return url;
                }
            });
        }

        public Task<Stat> Click(string segment, string referer, string ip)
        {
            return Task.Run(() =>
            {
                using (var ctx = new ShortnrContext())
                {
                    ShortUrl url = ctx.ShortUrls.Where(u => u.Segment == segment).FirstOrDefault();
                    if (url == null)
                    {
                        throw new ShortnrNotFoundException();
                    }

                    url.NumOfClicks++;

                    Stat stat = new Stat()
                    {
                        ClickDate = DateTime.Now,
                        Ip = ip,
                        Referer = referer,
                        ShortUrl = url
                    };
                    ctx.Stats.Add(stat);

                    ctx.SaveChanges();
                    
                    return stat;
                }
            });
        }

        private string NewSegment()
        {
            using (var ctx = new ShortnrContext())
            {
                int i = 0;
                int shortUrlLength = Convert.ToInt32(ConfigurationManager.AppSettings["ShortUrlLength"]);
                while (true)
                {
                    string segment = Guid.NewGuid().ToString().Substring(0, shortUrlLength);
                    if (!ctx.ShortUrls.Where(u => u.Segment == segment).Any())
                    {
                        return segment;
                    }
                    if (i > 30)
                    {
                        break;
                    }
                    i++;
                }
                return string.Empty;
            }
        }
    }
}
