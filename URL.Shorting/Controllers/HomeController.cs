using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using URL.Shorting.Data;
using URL.Shorting.Models;

namespace URL.Shorting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RedirectLink()
        {
            string urlShort = HttpContext.Request.Path.ToString();
            urlShort = urlShort.Replace("/Home/RedirectLink/", "");
            urlShort.Trim();
            var getUrl = _db.UrlTable.FirstOrDefault(x=> x.ShortUrl == urlShort);
            if (getUrl?.Url != null)
            {
                var numOfClick = getUrl.NumOfClick;
                numOfClick += 1;
                _db.UrlTable.FirstOrDefault(x=> x.ShortUrl == urlShort).NumOfClick = (int) numOfClick;
                _db.SaveChanges();
                return Redirect(getUrl.Url);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}