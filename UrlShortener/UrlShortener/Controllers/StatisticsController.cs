using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Entities;
using System.Net;

namespace UrlShortener.Controllers
{
    public class StatisticsController : Controller
    {
        private StatisticsContext db = new StatisticsContext();
        // GET: Statistics
        [HttpGet]
        public ViewResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ClicksSortParm = String.IsNullOrEmpty(sortOrder) ? "clicks_asc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var urls = db.ShortUrls.Select(s => s);
            switch (sortOrder)
            {
                case "clicks_asc":
                    urls = urls.OrderBy(s => s.NumOfClicks);
                    break;
                case "Date":
                    urls = urls.OrderBy(s => s.Added);
                    break;
                case "date_desc":
                    urls = urls.OrderByDescending(s => s.Added);
                    break;
                default:
                    urls = urls.OrderByDescending(s => s.NumOfClicks);
                    break;
            }
            return View(urls);
        }


        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            ShortUrl url = db.ShortUrls.Find(id);
            if (url == null)
            {
                return HttpNotFound();
            }
            string userName = UrlManager.GetUserName();
            if (userName != url.UserName)
            {
                throw new ArgumentException("Authorization check fail");
            }
            return View(url);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {                             
            try
            {
                ShortUrl url = db.ShortUrls.Where(u => u.Id == id).FirstOrDefault();
                db.ShortUrls.Remove(url);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index", "Statistics");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}