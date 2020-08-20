using System;
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
            if (context.Request.Cookies.ContainsKey("URL.Shorting"))
            {
                string shortUrlInCookies = context.Request.Cookies["URL.Shorting"];
                context.Response.Cookies.Append("URL.Shorting", $"{shortUrlInCookies};{urls.ShortUrl}");
            }
            else
            {
                context.Response.Cookies.Append("URL.Shorting", urls.ShortUrl);
            }
        }

        //Adding url, when is user sign in or log in
        public void AddUrlToDb(string identityName, List<Urls> toList, HttpContext context)
        {
            string shortUrlInCookies = context.Request.Cookies["URL.Shorting"];
            foreach (var item in toList)
            {
                if (shortUrlInCookies.Contains(item.ShortUrl))
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
            string shortUrlInCookies = context.Request.Cookies["URL.Shorting"];
            foreach (var item in toList)
            {
                if (shortUrlInCookies.Contains(item.ShortUrl))
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}