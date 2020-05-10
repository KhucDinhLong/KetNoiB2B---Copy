using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using SETA.Core.Configuration;

namespace SETA.Core.Localization
{
    public class Localizer
    {
        protected String m_tag;
        /// <summary>
        /// String tag that defines the languages (2 char)
        /// </summary>
        public String Tag
        {
            get { return m_tag; }
            set { m_tag = value; }
        }

        Dictionary<string, string> m_strings = new Dictionary<string, string>();

        // create singleton
        static readonly Localizer instance = new Localizer();
        /// <summary>
        /// Get instance of singleton.
        /// </summary>
        public static Localizer Instance
        {
            get
            {
                return instance;
            }
        }
        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit
        /// </summary>
        static Localizer()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        Localizer()
        {
            m_tag = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }

        public void Initialize(string filename)
        {
            XDocument xmlFile = XDocument.Load(filename);
            XElement root = xmlFile.Element(Config.GetConfigByKey("RootLanguage"));

            // read strings
            IEnumerable<XElement> texts = root.Descendants(Config.GetConfigByKey("ElementText"));
            if (texts != null)
            {
                texts.ToList().ForEach(text =>
                {
                    var items = text.Elements(Config.GetConfigByKey("ElementItem"));
                    if (items != null)
                    {
                        XElement locStringXml = items.FirstOrDefault(item => item.Attribute(Config.GetConfigByKey("ItemLang")).Value.ToString() == m_tag);
                        if (locStringXml != null)
                        {
                            var name = Config.GetConfigByKey("TextName");
                            var value = Config.GetConfigByKey("TextValue");
                            m_strings.Add(text.Attribute(name).Value.ToString(), locStringXml.Attribute(value).Value.ToString());
                        }
                    }
                });
            }
        }

        //public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        //{
        //    foreach (T element in source)
        //        action(element);
        //}

        public String GetText(string name)
        {
            if (m_strings.ContainsKey(name))
            {
                return m_strings[name];
            }
            return name;
        }
    }
}
