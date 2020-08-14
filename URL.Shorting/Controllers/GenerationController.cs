using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using URL.Shorting.Models;
using URL.Shorting.Services;
using URL.Shorting.ViewModels;

namespace URL.Shorting.Controllers
{
    public class GenerationController : Controller
    {

        private UrlContext _db;
        private readonly UrlService _urlService;

        public GenerationController(UrlContext db, UrlService urlService)
        {
            _db = db;
            _urlService = urlService;
        }
        
        [HttpGet]
        public IActionResult Generation()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult UrlList()
        {
            List<Urls> itemList;
            if (User.Identity.IsAuthenticated)
            {
                itemList = _db.UrlTable.Where(x => x.Username == User.Identity.Name).ToList();

            }
            else
            {
                itemList = _urlService.GetUrlsNoName(_db.UrlTable.Where(x => x.Username == null).ToList(), HttpContext);
            }
            foreach (var item in itemList)
            {
                item.ShortUrl = "https://" + HttpContext.Request.Host + "/" + item.ShortUrl;
            }
            return View(itemList);
        }

        [HttpGet]
        public IActionResult UrlReturn(string result)
        {
            return View(result);
        }
        
        [HttpPost]
        public IActionResult Generation(GenerationViewModel model)
        {
            var context = HttpContext;
            if (ModelState.IsValid)
            {
                string shortUrl = GetRandomUrl();
                while (_db.UrlTable.Any(ur => ur.ShortUrl == shortUrl))
                {
                    shortUrl = GetRandomUrl();
                }
                var url = new Urls();
                url.Url = model.Url;
                url.NumOfClick = 0;
                url.ShortUrl = shortUrl;
                if (User.Identity.IsAuthenticated)
                {
                    url.Username = User.Identity.Name;
                    _db.UrlTable.Add(url);
                    _db.SaveChanges();
                }
                else
                {
                    url.Username = null;
                    _urlService.AddUrl(url, context);
                }
                return View("UrlReturn", "https://"+HttpContext.Request.Host+"/"+shortUrl);
            }
            
            return View();
        }
        
        public string GetRandomUrl()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }
        
    }
}
