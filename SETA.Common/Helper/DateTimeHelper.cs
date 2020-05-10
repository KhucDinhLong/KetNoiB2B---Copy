using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Helper
{
    public class DateTimeHelper
    {
        public static DateTime ConvertToUtcDateTime(DateTime currentDateTime)
        {
            return currentDateTime.ToUniversalTime();
        }

        public static DateTime ConvertDateTimeFromUTCToCurrentClient(DateTime datetime)
        {
            var currentTimeZone = TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, currentTimeZone);
        }

        public static DateTime ConvertUtcToClientDateTime(DateTime utcDateTime, int timeOffset = 0)
        {            
            return utcDateTime.AddMinutes(timeOffset);            
        }

        public static DateTime ConvertFromUtcDateTime(DateTime utcDateTime, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(utcDateTime, TimeZoneInfo.Utc, timeZoneInfo);
        }
    }
}
