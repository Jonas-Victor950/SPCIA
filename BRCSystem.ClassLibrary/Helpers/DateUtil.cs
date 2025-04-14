using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.Helpers
{
    public static class DateUtil
    {
        public static DateTime convertStringToDateTime(string date, string format)
        {

            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime convertedDate = DateTime.ParseExact(date, format, CultureInfo.CurrentUICulture);
            return TimeZoneInfo.ConvertTimeFromUtc(convertedDate, kstZone).ToUniversalTime();

        }
        public static DateTime calcDateFromWeek(string yyww)
        {
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            int year = int.Parse(yyww.Substring(0, 2));
            int week = int.Parse(yyww.Substring(2, 2));

            DateTime jan1 = new DateTime(2000 + year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            DateTime calWeek = firstThursday.AddDays(week * 7);

            DateTime finalDate = calWeek.AddDays(-3);

            finalDate = GetFirstDayOfWeek(finalDate);

            return TimeZoneInfo.ConvertTimeFromUtc(finalDate, kstZone).ToUniversalTime();
        }

        private static DateTime GetFirstDayOfWeek(DateTime initialDate)
        {
            int dayOfWeek = (int)initialDate.DayOfWeek;

            DateTime finalDate = initialDate.AddDays(-dayOfWeek + 1);

            return finalDate;
        }

        public static int GetDaysDifferenceDates(DateTime startDate, DateTime endDate)
        {
            TimeSpan differenceDates = endDate.Date - startDate.Date;
            int differenceDays = (int)differenceDates.TotalDays;
            return differenceDays;
        }

        public static int GetminutesDifferenceDates(DateTime startDate, DateTime endDate)
        {
            TimeSpan differenceDates = startDate - endDate;
            int differenceMinutes = (int)differenceDates.TotalMinutes;
            return differenceMinutes;
        }

        public static string GenerateUniqueCode()
        {
            DateTime currentTime = DateTime.UtcNow;
            string code = currentTime.ToString("MMddHHmmss");
            return code;
        }

    }
}