using System.Collections.Generic;
using System.Linq;
using URL.Shorting.Models;
using Microsoft.AspNetCore.Http;
using URL.Shorting.Data;

namespace URL.Shorting.Services
{
    public class UrlService
    {
        private readonly ApplicationContext _db;
        public UrlService(ApplicationContext context)
        {
            _db = context;
        }
        
        //Adding url of an unauthorized user
        public void AddUrl(Urls urls, HttpContext context)
        {
            _db.UrlTable.Add(urls);
            _db.SaveChanges();
            context.Response.Cookies.Append(urls.ShortUrl, urls.ShortUrl);
        }

        //Adding url, when is user sign in or log in
        public void AddUrlToDb(string identityName, List<Urls> toList, HttpContext context)
        {
            foreach (var item in toList)
            {
                string shortUrl = null;
                if (context.Request.Cookies.TryGetValue(item.ShortUrl, out shortUrl))
                {
                    _db.UrlTable.FirstOrDefault(x => x.ShortUrl == item.ShortUrl).Username = identityName;
                    _db.SaveChanges();
                    context.Response.Cookies.Delete(item.ShortUrl);
                }
            }
        }
 
        //Method for getting a list of generated links from cookies
        public List<Urls> GetUrlsNoName(List<Urls> toList, HttpContext context)
        {
            var items = new List<Urls>();
            foreach (var item in toList)
            {
                string shortUrl = null;
                if (context.Request.Cookies.TryGetValue(item.ShortUrl, out shortUrl))
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}