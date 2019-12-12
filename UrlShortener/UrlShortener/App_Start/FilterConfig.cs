using System.Web;
using System.Web.Mvc;
using UrlShortener.Filters;

namespace UrlShortener
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ShortnrErrorFilter());
        }
    }
}
