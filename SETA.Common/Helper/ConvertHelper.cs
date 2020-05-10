using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace SETA.Common.Helper
{
    public class ConvertHelper
    {
        public static List<object> ConvertAll<T>(IList<T> fromObjects, System.Type toType)
        {
            if (fromObjects != null)
            {
                var test = Activator.CreateInstance(toType);
                var list = new List<object>();

                foreach (object obj in fromObjects)
                {
                    list.Add(Convert(obj, toType));
                }

                return list;
            }
            return null;
        }

        public static object Convert<T>(T fromObject, System.Type toType)
        {
            if (fromObject == null) return null;
            object returnObject = Activator.CreateInstance(toType);

            PropertyInfo[] infos = returnObject.GetType().GetProperties();
            foreach (PropertyInfo property in infos)
            {
                PropertyMap[] attributes =
                    (PropertyMap[])property.GetCustomAttributes(typeof(PropertyMap), false);
                if (!attributes.Any())
                {
                    attributes = new PropertyMap[1];
                    attributes[0] = new PropertyMap(property.Name);
                }
                if (attributes.Length > 0)
                {
                    PropertyInfo fromProperty =
                        fromObject.GetType().
                            GetProperty(attributes[0].ValueFromProperty);

                    if (fromProperty != null && attributes[0].FromType == null)
                    {
                        property.SetValue(returnObject,
                            fromProperty.GetValue(fromObject, null), null);
                    }
                    else if (fromProperty != null)
                    {
                        //if (fromProperty.PropertyType.IsArray)
                        if (fromProperty.PropertyType.Name.ToLower().Contains("list"))
                        {
                            var res = ((IList)fromProperty.GetValue(fromObject, null)).Cast<T>().ToList();
                            var newvalue = ConvertAll(res, attributes[0].ToType);
                            property.SetValue(returnObject, newvalue, null);
                        }
                        else
                        {
                            property.SetValue(returnObject,
                                Convert(fromProperty.GetValue(fromObject,
                                    null), property.PropertyType), null);
                        }
                    }
                }
            }

            return returnObject;
        }

        public static List<T2> ConvertList<T1, T2>(IList<T1> fromObjects)
        {
            if (fromObjects != null)
            {
                var list = Activator.CreateInstance<List<T2>>();
                foreach (var obj in fromObjects)
                {
                    list.Add(ConvertObj<T1, T2>(obj));
                }

                return list;
            }
            return null;
        }

        public static T2 ConvertObj<T1, T2>(T1 fromObject)
        {
            var returnObject = Activator.CreateInstance<T2>();

            PropertyInfo[] infos = returnObject.GetType().GetProperties();
            foreach (PropertyInfo property in infos)
            {
                PropertyMap[] attributes =
                    (PropertyMap[])property.GetCustomAttributes(typeof(PropertyMap), false);
                if (!attributes.Any())
                {
                    attributes = new PropertyMap[1];
                    attributes[0] = new PropertyMap(property.Name);
                }
                if (attributes.Length > 0)
                {
                    PropertyInfo fromProperty =
                        fromObject.GetType().
                            GetProperty(attributes[0].ValueFromProperty);

                    if (fromProperty != null && attributes[0].FromType == null)
                    {

                        property.SetValue(returnObject,
                            fromProperty.GetValue(fromObject, null), null);
                    }
                    else if (fromProperty != null)
                    {
                        //if (fromProperty.PropertyType.IsArray)
                        if (fromProperty.PropertyType.Name.ToLower().Contains("list"))
                        {
                            var res = ((IList)fromProperty.GetValue(fromObject, null)).Cast<T1>().ToList();
                            var newvalue = ConvertList<T1, T2>(res);
                            property.SetValue(returnObject, newvalue, null);
                        }
                        else
                        {
                            property.SetValue(returnObject,
                                Convert(fromProperty.GetValue(fromObject, null), property.PropertyType), null);
                        }
                    }
                }
            }

            return returnObject;
        }

        public static string ObjectToStringJson(object json)
        {
            return JsonConvert.SerializeObject(json, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ObjectCreationHandling = ObjectCreationHandling.Reuse,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                    //Formatting = Newtonsoft.Json.Formatting.Indented,

                });
        }

        public static string ObjectToJson(object json)
        {
            return JsonConvert.SerializeObject(json);
        }

        public static string ToString(object obj)
        {
            string retVal = "";

            try
            {
                retVal = System.Convert.ToString(obj);
            }
            catch
            {
                retVal = "";
            }

            return retVal;
        }

        public static int ToInt32(object obj)
        {
            int retVal = 0;

            try
            {
                retVal = System.Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static double ToDouble(object obj)
        {
            double retVal = 0;

            try
            {
                retVal = System.Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static decimal ToDecimal(object obj)
        {
            decimal retVal = 0;

            try
            {
                retVal = System.Convert.ToDecimal(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static long ToLong(object obj)
        {
            long retVal = 0;

            try
            {
                retVal = System.Convert.ToInt64(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static DateTime ConvertToDate(object obj, DateTime defaultDateTime)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch
            {
                return defaultDateTime;
            }
        }
        public static DateTime ConvertToDateTimeUTC(DateTime clientDate, double timeZone, bool isHours)
        {
            DateTime dateUTC = new DateTime();
            try
            {
                TimeSpan offset = new TimeSpan();

                if (isHours)
                {
                    offset = TimeSpan.FromHours(timeZone);
                }
                else
                {
                    offset = TimeSpan.FromHours(timeZone);
                }

                var timeUTC = DateTime.Parse(DateTime.Now.ToUniversalTime().ToString("hh:mm:ss")).TimeOfDay;
                dateUTC = clientDate.Add(timeUTC).Add(offset);
            }
            catch
            {
                return dateUTC;
            }
            return dateUTC;
        }
        public static string ReplaceStringByRegex(string str, string regex, string replaceBy)
        {
            RegexOptions options = RegexOptions.None;
            Regex nRegex = new Regex(regex, options);
            return nRegex.Replace(str, replaceBy);
        }

        public static string UnicodeToAscii(string unicodeString)
        {
            if (!string.IsNullOrEmpty(unicodeString))
            {
                Encoding ascii = Encoding.ASCII;
                Encoding unicode = Encoding.Unicode;

                // Convert the string into a byte array.
                byte[] unicodeBytes = unicode.GetBytes(unicodeString);

                // Perform the conversion from one encoding to the other.
                byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

                // Convert the new byte[] into a char[] and then into a string.
                char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                return new string(asciiChars);
            }
            return string.Empty;
        }

        //public static string ReplaceMultiSpace(string str)
        //{
        //    RegexOptions options = RegexOptions.None;
        //    Regex regex = new Regex(@"[ ]{1,}", options);
        //    return regex.Replace(str, @"_");
        //}

        //public static string ReplaceSpecialCharacter(string str)
        //{
        //    return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "");
        //}

        public static string PartialView(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
        public static string RenderPartialView(Controller controller, string viewName, object model, HttpContextBase contextBase)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var context = contextBase;
                var routeData = new RouteData();
                //routeData.Values.Add(("someRouteDataProperty", "someValue");

                var controllerContext = new ControllerContext(new RequestContext(context, controller.RouteData), controller);                               

                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
        public static string GetRazorViewAsString(Controller controller, string view, object model)
        {
            var guid = Guid.NewGuid();
            var filePath = AppDomain.CurrentDomain.BaseDirectory + guid + ".cshtml";
            File.WriteAllText(filePath, string.Format("@inherits System.Web.Mvc.WebViewPage<{0}>\r\n {1}", model.GetType().FullName, view));
            var st = new StringWriter();
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(context, routeData), controller);
            var razor = new RazorView(controllerContext, "~/" + guid + ".cshtml", null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), st), st);
            File.Delete(filePath);
            return st.ToString();
        }

        public static List<long> StringToList(string list)
        {
            List<long> _list = new List<long>();
            string[] listElement = list.Split(',');
            foreach (string element in listElement)
            {
                _list.Add(ConvertHelper.ToLong(element));
            }
            return _list;
        }
        public static string ListToString(List<long> list)
        {
            if(list.Count == 0)
            {
                return "0";
            }
            var str =
            list.Select(i => i.ToString(CultureInfo.InvariantCulture))
                .Aggregate((s1, s2) => s1 + ", " + s2);
            return str;
        }

        public static string HomeworkDueToDetail(string homeworkDue)
        {
            try
            {
                var dateDue = DateTime.Parse(homeworkDue);
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;

                //if (cal.GetWeekOfYear(dateDue, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                //{
                //    return "this " + dateDue.DayOfWeek;
                //}
                //else
                //{
                    return dateDue.ToString("MMM dd, yyyy");
                //}
            }
            catch (Exception)
            {
                return string.Empty;
            }                        
        }

        public static string HomeworkDueToDayOfWeek(string homeworkDue)
        {
            try
            {
                return DateTime.Parse(homeworkDue).ToString("dddd MMM dd, yyyy");
            }
            catch (Exception)
            {
                return string.Empty;
            }                       
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyMap : Attribute
    {
        private string _propertyName = string.Empty;
        private System.Type _fromType = null;
        private System.Type _toType = null;

        public PropertyMap(string propertyName)
        {
            this._propertyName = propertyName;
        }

        public PropertyMap(string propertyName, System.Type
            fromType)
        {
            this._propertyName = propertyName;
            this._fromType = fromType;
        }

        public PropertyMap(string propertyName, System.Type fromType,
            System.Type toType)
        {
            this._propertyName = propertyName;
            this._fromType = fromType;
            this._toType = toType;
        }

        public string ValueFromProperty
        {
            get { return this._propertyName; }
        }

        public System.Type FromType
        {
            get { return this._fromType; }
        }

        public System.Type ToType
        {
            get { return this._toType; }
        }
    }
}
