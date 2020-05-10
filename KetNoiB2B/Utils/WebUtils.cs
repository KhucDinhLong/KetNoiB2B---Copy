using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KetNoiB2B.Utils
{
    public class WebUtils
    {
        //public static string GenerateImageUrl(string linkUrl)
        //{
        //    var path = string.Format("{0}\\{1}", Server.MapPath(@"\"), productImage.ImageUrl);
        //}
        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}