using System;
using System.Globalization;

namespace SchoolLib.Data.Extensions
{
    public static class StringToDate
    {
        static IFormatProvider culture = new CultureInfo("uk-UA");
        public static bool TryParse(this string str_date, out DateTime date)
        {
            return DateTime.TryParseExact(str_date, "dd.MM.yyyy", culture,
                DateTimeStyles.AssumeLocal, out date);
        }
        public static DateTime Parse(this string str_date)
        {
            return DateTime.ParseExact(str_date, "dd.MM.yyyy", culture,
                DateTimeStyles.AssumeLocal);
        }
    }
}
