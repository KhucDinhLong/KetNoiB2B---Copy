﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SETA.Core.Helper.Uri
{
    public static class UriHelper
    {
        public static bool IsHashURL()
        {
            return HttpContext.Current.Request.QueryString["hashtable"] == "true";
        }
        public static string GetCurrentHost()
        {
            var s = HttpContext.Current.Request.Url;
            var k = s.Scheme + "://" + s.Authority + HttpContext.Current.Request.ApplicationPath;
            if (!k.EndsWith("/"))
            {
                k = k + "/";
            }
            return k;
        }

        public static string GetFullUrl(HttpRequestBase request)
        {
            var builder = new StringBuilder();
            builder.Append(request.Url.Scheme);
            builder.Append("://");
            builder.Append(request.Url.Authority);

            if ((request.Url.Authority.EndsWith("/") && !request.ApplicationPath.StartsWith("/"))
                || (!request.Url.Authority.EndsWith("/") && request.ApplicationPath.StartsWith("/")))
            {
                builder.Append(request.ApplicationPath);
            }
            else if (!request.Url.Authority.EndsWith("/") && !request.ApplicationPath.StartsWith("/"))
            {
                builder.Append("/");
                builder.Append(request.ApplicationPath);
            }
            else if (request.ApplicationPath.Length > 1)
            {
                builder.Append(request.ApplicationPath.Substring(1));
            }
            var url = builder.ToString();
            var test = url.EndsWith("/");
            if (!test) builder.Append("/");
            return builder.ToString();
        }
    }
}
