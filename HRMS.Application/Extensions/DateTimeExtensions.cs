using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Extensions
{
    public static class DateTimeExtensions
    {

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 - (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(1 * diff).Date;
        }
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
        public static DateTime ToIndianStandardTime(this DateTime dateTime)
        {
            DateTime utcTime = dateTime.ToUniversalTime(); // From current datetime I am retriving UTC time
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"); // Now I am Getting `IST` time From `UTC`
            DateTime yourISTTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, istZone);
            return yourISTTime;
        }
    }
}
