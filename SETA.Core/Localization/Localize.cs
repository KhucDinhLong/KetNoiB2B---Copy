using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using SETA.Core.Configuration;

namespace SETA.Core.Localization
{
    public class Localize
    {
        private static Dictionary<string, Resource> Resources = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="Localize"/> class from being created.
        /// </summary>
        private Localize()
        {
            AppendFiles(AppDomain.CurrentDomain.BaseDirectory + Config.GetConfigByKey("DirectoryLanguage"), "en");
        }

        /// <summary>
        /// Checks the resources.
        /// </summary>
        private static void CheckResources()
        {
            if (Resources == null)
                Resources = new Dictionary<string, Resource>();
        }

        /// <summary>
        /// "Add single file to resources"
        /// </summary>
        /// <param name="FullFilePath"></param>
        /// <param name="lang"></param>
        public static void AddFile(string FullFilePath, string lang)
        {
            CheckResources();
            Resources.Add(GetKey(FullFilePath, lang), new Resource(FullFilePath, lang));
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="fullFilePath">The full file path.</param>
        /// <param name="lang">The lang.</param>
        /// <returns></returns>
        private static string GetKey(string fullFilePath, string lang)
        {
            string key = Path.GetFileNameWithoutExtension(fullFilePath);
            if (lang != "")
            {
                if (key.IndexOf("_" + lang) != -1)
                {
                    return key.Substring(0, key.Length - lang.Length - 1);
                }
            }

            return key;
        }

        /// <summary>
        /// "All resource file will be read with .resxml extension, which contains no '_' char in its filename."
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="lang"></param>
        public void GetFiles(string dirPath, string lang)
        {
            CheckResources();
            Resources.Clear();
            AppendFiles(dirPath, lang);
        }

        /// <summary>
        /// "All resource file will be read with .resxml extension, which contains no '_' char in its filename."
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="lang"></param>
        public Dictionary<string, Resource> GetFilesLoaded()
        {
            if (Resources.Count == 0) return null;
            return Resources;
        }

        /// <summary>
        /// "Get files in director and append them to resources"
        /// </summary>
        /// <param name="fullDirPath"></param>
        /// <param name="lang"></param>       
        public void AppendFiles(string fullDirPath, string lang)
        {
            CheckResources();
            string[] files = Directory.GetFiles(fullDirPath, "*." + Config.GetConfigByKey("ExtLanguage"), SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var key = GetKey(file, lang);
                if (file.IndexOf("_") == -1 && !Resources.ContainsKey(key))
                    Resources.Add(key, new Resource(file, lang));
            }
        }

        /// <summary>
        /// "Get value of node (id) in Resource (res)."
        /// </summary>
        /// <param name="res"></param>
        /// <param name="id"></param>
        /// <returns></returns>       
        public string GetValue(string res, string id)
        {
            // ups no resource
            if (Resources == null)
                return id;

            Resource Res = null;
            if (!Resources.TryGetValue(res, out Res))
                return id;

            return Res.GetValue(id);
        }

        public string GetValue(string res, string id, string key)
        {
            return String.Format(GetValue(res, id), key);
        }

        public static string[] FileNames()
        {
            string[] strs = new string[Resources.Count];
            int i = 0;
            foreach (KeyValuePair<string, Resource> kv in Resources)
                strs[i++] = kv.Value.FileName;

            return strs;
        }
    }

    // internal class
    /// <summary>
    /// Resource Language
    /// </summary>
    public class Resource
    {
        public string FileName = "";
        public string FilePath = "";
        public string Language = "";

        public XmlDocument xDoc = null;
        public XmlElement xRoot = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="lang">The lang.</param>
        public Resource(string fullPath, string lang)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = fullPath;
            Language = lang;

            OpenXmlFile();
        }

        /// <summary>
        /// Sets the language.
        /// </summary>
        /// <param name="lan">The lan.</param>
        public void SetLanguage(string lan)
        {
            Language = lan;
            OpenXmlFile();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string GetValue(string id)
        {
            if (xDoc == null)
                return id;

            foreach (XmlNode node in xRoot.GetElementsByTagName(Config.GetConfigByKey("ElementText")))
            {
                if (node.Attributes != null && node.Attributes[Config.GetConfigByKey("TextName")].Value == id)
                    return node.Attributes["value"].Value;
            }
            return id;
        }

        ///// <summary>
        ///// Gets the value.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="key">The key.</param>
        ///// <returns></returns>
        //public string GetValue(string id, string key)
        //{
        //    return String.Format(GetValue(id), key);
        //}

        /// <summary>
        /// Opens the XML file.
        /// </summary>
        private void OpenXmlFile()
        {
            // try to open file width language set
            if (!TryToOpen(GetPath()))
                // try it again
                TryToOpen(FilePath);
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            // no language set
            if (Language == "")
                return FilePath;

            return Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FileName) + "_" + Language + Path.GetExtension(FileName);
        }

        /// <summary>
        /// Tries to open.
        /// </summary>
        /// <param name="fullpath">The fullpath.</param>
        /// <returns></returns>
        private bool TryToOpen(string fullpath)
        {
            if (!File.Exists(fullpath))
                return false;

            try
            {
                xDoc = new XmlDocument();
                xDoc.Load(fullpath);
                xRoot = xDoc.DocumentElement;

                return true;
            }
            catch
            {
                // ups some error
                xDoc = null;
            }
            return false;
        }
    }
}
