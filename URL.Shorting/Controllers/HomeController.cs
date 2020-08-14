using System.Linq;
using Microsoft.AspNetCore.Mvc;
using URL.Shorting.Models;

namespace URL.Shorting.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlContext _db;

        public HomeController(UrlContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RedirectLink()
        {
            string url = HttpContext.Request.Path.ToString();
            url = url.Replace("/Home/RedirectLink/", "");
            url.Trim();
            if (_db.UrlTable.FirstOrDefault(x => x.ShortUrl == url)?.Url != null)
            {
                var numOfClick = _db.UrlTable.FirstOrDefault(x => x.ShortUrl == url)?.NumOfClick;
                if (numOfClick != null)
                {
                    numOfClick += 1;
                    _db.UrlTable.FirstOrDefault(x => x.ShortUrl == url).NumOfClick = (int) numOfClick;
                    _db.SaveChanges();
                }
                string longUrl = _db.UrlTable.FirstOrDefault(x => x.ShortUrl == url)?.Url;
                return Redirect(longUrl);
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