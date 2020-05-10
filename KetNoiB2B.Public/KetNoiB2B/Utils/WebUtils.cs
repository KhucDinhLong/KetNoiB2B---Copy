using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KetNoiB2B.Utils
{
    public class WebUtils
    {
        public static string GetImagePath(string imageUrl)
        {
            var result = imageUrl;
            var domainImage = "http://admin.ketnoib2b.com";
            if (!string.IsNullOrEmpty(imageUrl))
            {
                result = domainImage + imageUrl;
            }
            else
            {
                result = "/img/no_thumbnail.jpg";
            }

            return result;
        }
    }
}