﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.SiteMain.Utilities
{
    public class CookiesManeger
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

        public string GetValue(HttpContext context, string token)
        {
            string cookieValue;
            if (!context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return null;
            }
            return cookieValue;
        }

        public Guid GetBrowserId(HttpContext context)
        {
            string broserId = GetValue(context, "BrowserId");
            if(broserId == null)
            {
                string value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", value);
                broserId = value;
            }
            Guid guidBroserId;
            Guid.TryParse(broserId, out guidBroserId);
            return guidBroserId;
        }
        public void Remove(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
            {
                context.Response.Cookies.Delete(token);
            }
        }


        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100),
            };
        }
    }
}
