using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SETA.Common
{
    public static class EnumHelper
    {
        
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the DescriptionAttribute attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            //// Get the stringvalue attributes
            DescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            //// Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Description : value.ToString();
        }

        public static string GetEnumDescription<T>(this T valueType)
        {
            // Get the type
            Enum value = valueType as Enum;

            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            //// Get the stringvalue attributes
            System.ComponentModel.DescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute[];

            //// Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Description : value.ToString();
        }
        
        /// <summary>
        /// Get DescriptionAttribute string
        /// </summary>
        /// <param name="value"> Enum value</param>
        /// <returns> Return DescriptionAttribute, if haven't DescriptionAttribute will return value.tostring </returns>
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// Get enum value by DescriptionAttribute
        /// </summary>
        /// <param name="value"> DescriptionAttribute value </param>
        /// <param name="enumType"> Enum type </param>
        /// <returns> Return enum value</returns>
        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

    }

    public class BaseEnumElement
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }

    }
    public class ConvertKeyName
    {
        public static char[] Chars = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public static string Convert(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "#";
            }
            var key = name.ToLower().Trim().Length > 0 ? name.ToLower().Trim().ToCharArray()[0] : '#';
            return Chars.Contains(key) ? key.ToString() : "#";
        }
    }
    public static class EnumHelperExt<T>
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}

