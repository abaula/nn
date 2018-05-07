using System;
using System.Globalization;

namespace WinFormsApp.Extensions
{
    static class DateTimeExtensions
    {
        public static int WeekOfYear(this DateTime date)
        {
            return CultureInfo.InvariantCulture
                .Calendar
                .GetWeekOfYear(date,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);
        }

        public static float TicksToHour(this double ticks)
        {
            return (float)ticks / TimeSpan.TicksPerHour;
        }
    }
}
