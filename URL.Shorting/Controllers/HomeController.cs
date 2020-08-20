using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using URL.Shorting.Data;

namespace URL.Shorting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult RedirectLink(string id) // ID is short url 
        {
            var getUrl = _db.UrlTable.FirstOrDefault(x=> x.ShortUrl == id);
            if (getUrl?.Url != null)
            {
                var numOfClick = getUrl.NumOfClick;
                numOfClick += 1;
                _db.UrlTable.FirstOrDefault(x=> x.ShortUrl == id).NumOfClick = (int) numOfClick;
                _db.SaveChanges();
                return Redirect(getUrl.Url);
            }
            else
            {
                return View("Error");
            }
        }

        [Route("/Error")]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}