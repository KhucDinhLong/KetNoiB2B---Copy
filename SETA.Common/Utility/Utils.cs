using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using SETA.Common.Constants;

namespace SETA.Common.Utility
{
    public class Utils
    {
        public static string GetDateTimeFormat(DateTime? datetime, string formatString = "{0:MM/dd/yyyy hh:mm tt}")
        {
            return (datetime == null || datetime.Equals(DateTime.MinValue))
                       ? string.Empty
                       : String.Format(formatString, datetime.Value);
        }

        public static string GetDateFormat(DateTime? datetime)
        {
            return datetime == null ? string.Empty : String.Format("{0:MM/dd/yyyy}", datetime.Value);
        }
        public static string GetTimeFormat(DateTime? datetime)
        {
            return datetime == null ? string.Empty : String.Format("{0:hh:mm tt}", datetime.Value);
        }

        public static string GetControlId(object oString)
        {
            var id = ParseData.GetString(oString);
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString().Replace("-", "");
            }
            return id;
        }

        public static string ObjectToStringJson(object json)
        {
            return JsonConvert.SerializeObject(json, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public static string StringScrubHtml(string value)
        {
            var step2 = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
                step2 = Regex.Replace(step1, @"\s{2,}", " ");   
            }            
            return step2;
        }

        /// <summary>
        /// Get setting from AppSettings node with default value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default setting value</param>
        /// <returns></returns>
        public static T GetSetting<T>(string key, T defaultValue)
        {
            try
            {
                string appSetting = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrEmpty(appSetting)) return defaultValue;

                return (T)Convert.ChangeType(appSetting, typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static bool IsVideoFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".mp4": return true;
                case ".flv": return true;
                case ".3gp": return true;
                case ".avi": return true;
                case ".dat": return true;
                case ".mkv": return true;
                case ".m4v": return true;
                case ".mov": return true;
                case ".mpg": return true;
                case ".mpeg": return true;
                case ".wmv": return true;
                default: return false;
            }
        }

        public static bool IsAudioFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".gp5": return true;
                case ".wav": return true;
                case ".mp3": return true;
                case ".arm": return true;
                case ".wma": return true;
                case ".m4p": return true;
                default: return false;
            }
        }

        public static bool IsDocumentFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".pdf": return true;
                case ".xml": return true;
                default: return false;
            }
        }

        public static bool IsExcelFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".xls": return true;
                case ".xlsx": return true;
                case ".xla": return true;
                case ".csv": return true;
                default: return false;
            }
        }

        public static bool IsWordFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".doc": return true;
                case ".docx": return true;
                default: return false;
            }
        }

        public static bool IsPowerPointFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".ppt": return true;
                case ".pptx": return true;
                default: return false;
            }
        }

        public static bool IsTextFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".txt": return true;
                default: return false;
            }
        }

        public static bool IsSQLFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".sql": return true;
                default: return false;
            }
        }

        public static bool IsImageFile(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg": return true;
                case ".jpeg": return true;
                case ".png": return true;
                case ".bmp": return true;
                case ".gif": return true;
                case ".tif": return true;
                case ".jpe": return true;
                case ".tiff": return true;
                default: return false;
            }
        }

        public static string CompareSelectedDay(int value, int day)
        {
            return (value == day) ? "selected='selected'" : "";
        }

        /// <summary>
        /// Compare object
        /// <author>SuNV</author><date>2015/08/27</date>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static bool IsEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            #region normal
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
            #endregion
            ////Case: by Linq
            //if (self != null && to != null)
            //{
            //    var type = typeof(T);
            //    var ignoreList = new List<string>(ignore);
            //    return
            //        !(from pi in
            //            type.GetProperties(System.Reflection.BindingFlags.Public |
            //                               System.Reflection.BindingFlags.Instance)
            //            where !ignoreList.Contains(pi.Name)
            //            let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
            //            let toValue = type.GetProperty(pi.Name).GetValue(to, null)
            //            where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
            //            select selfValue).Any();
            //}
            //return self == to;
        }

        public static IEnumerable<SelectListItem> CreateSelectList<T>(IList<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            return entities
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                });
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static bool IsTablet(string userAgent, bool isMobile)
        {
            Regex r = new Regex("ipad|android|android 3.0|xoom|sch-i800|playbook|tablet|kindle|nexus");
            bool isTablet = r.IsMatch(userAgent) && isMobile;
            return isTablet;
        }

        public static MemberChange CompareLstMemberChange(List<long> self, List<long> to)
        {
            if (self == null)
            {
                self = new List<long>();
            }
            if (to == null)
            {
                to = new List<long>();
            }
            MemberChange memberChange = new MemberChange();
            List<long> listDeleteId = self.Except(to).ToList();
            List<long> listNewId = to.Except(self).ToList();
            if (listDeleteId.Count > 0 || listNewId.Count > 0)
            {
                memberChange.Change = true;
                memberChange.ListDeleteId = listDeleteId;
                memberChange.ListNewId = listNewId;
                return memberChange;
            }
            else
            {
                memberChange.Change = false;
                return memberChange;
            }
        }
        public static MemberChange CompareMemberIDChange(long self, long to)
        {
            if (self == null)
            {
                self = 0;
            }
            if (to == null)
            {
                to = 0;
            }

            MemberChange memberChange = new MemberChange();
            long deleteId = (self != to) ? self : 0;
            long newId = (to != self) ? to : 0;
            List<long> delete = new List<long>();
            List<long> add = new List<long>();
            if (deleteId != 0 || newId != 0)
            {
                delete.Add(deleteId);
                add.Add(newId);
                memberChange.Change = true;
                memberChange.ListDeleteId = delete;
                memberChange.ListNewId = add;
                return memberChange;
            }
            else
            {
                memberChange.Change = false;
                return memberChange;
            }
        }

    }

    public class MemberChange
    {
        public bool Change { get; set; }
        public List<long> ListDeleteId { get; set; }
        public List<long> ListNewId { get; set; }
        public List<long> ListReferenceId { get; set; }
    }
}
