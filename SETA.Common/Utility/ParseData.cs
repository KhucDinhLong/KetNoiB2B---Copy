using System;
namespace SETA.Common.Utility
{
    public class ParseData
    {
        public static int? GetInt(object oInt)
        {
            if (oInt == null)
            {
                return null;
            }
            try
            {
                return int.Parse(oInt.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static decimal GetDecimal(object oDecimal)
        {
            if (oDecimal == null)
            {
                return 0;
            }
            try
            {
                return decimal.Parse(oDecimal.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static float GetFloat(object oFloat)
        {
            if (oFloat == null)
            {
                return 0;
            }
            try
            {
                return float.Parse(oFloat.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static double? GetDouble(object oDouble)
        {
            if (oDouble == null)
            {
                return null;
            }
            try
            {
                return double.Parse(oDouble.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool GetBool(object oBool)
        {
            if (oBool == null)
            {
                return false;
            }
            try
            {
                return bool.Parse(oBool.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetString(object oText)
        {
            if (oText == null)
            {
                return string.Empty;
            }
            try
            {
                return oText.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string Replace(object baseText, object replaceText, object byText)
        {
            if (baseText == null || replaceText == null || byText == null||baseText.ToString().Length == 0 || replaceText.ToString().Length == 0)
            {
                return string.Empty;
            }
            try
            {
                return baseText.ToString().Replace(replaceText.ToString(), byText.ToString());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static DateTime? GetDateTime(object oDateTime)
        {
            if (oDateTime == null)
            {
                return null;
            }
            try
            {
                return DateTime.Parse(GetString(oDateTime));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
