using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SETA.Common.Helper
{
    public class StringHelper
    {
        public static string[] ReadFromFile(string filePath)
        {
            try
            {
                return System.IO.File.ReadAllLines(filePath);
            }   
            catch (Exception)
            {
                return null;
            }            
        }

        public static string GetConfigByKeyFromTextFile(string filePath, string keyConfig, char charSplit)
        {
            var lines = ReadFromFile(filePath);
            if (lines != null && lines.Length > 0)
            {
                foreach (var line in lines)
                {
                    var arrLine = line.Split(charSplit);
                    if (arrLine.Length > 1 && string.CompareOrdinal(arrLine[0].ToLower(), keyConfig.ToLower()) == 0)
                    {
                        return arrLine[1];
                    }
                }
            }

            return string.Empty;
        }

        public static string GetYoutubeIdFromUrl(string youtubeUrl)
        {
            try
            {
                Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match youtubeMatch = YoutubeVideoRegex.Match(youtubeUrl);

                string id = string.Empty;

                if (youtubeMatch.Success)
                    id = youtubeMatch.Groups[1].Value;

                return id;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }
        /// <summary>
        /// SuNV added 2016-10-2015: check html tag
        /// </summary>
        /// <param name="htmlCheck"></param>
        /// <returns></returns>
        public static bool HasHtmlTag(string htmlCheck)
        {
            try
            {
                var tagWithoutClosingRegex = new Regex(@"<[^>]+>");
                var hasTags = tagWithoutClosingRegex.IsMatch(htmlCheck);
                return hasTags;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// SuNV added 2017-06-29: GetRazorViewAsString
        /// GetRazorViewAsString
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetRazorViewAsString(object model, string filePath)
        {
            var st = new StringWriter();
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(context, routeData), new FakeController());
            var razor = new RazorView(controllerContext, filePath, null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), st), st);
            return st.ToString();
        }
    }
    public class FakeController : Controller
    {

    }
}
