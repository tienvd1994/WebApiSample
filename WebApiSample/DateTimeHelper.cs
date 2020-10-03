using System;
using System.Globalization;
using System.Linq;

namespace S0D.Infrastructure.Helpers
{
    public static class DateTimeHelper
    {
        public static byte GetQuarter(this DateTime date)
        {
            return (byte)((date.Month + 2) / 3);
        }

        public static int NumberOfParticularDaysInMonth(int year, int month, DayOfWeek dayOfWeek)
        {
            var totalDays = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(item => new DateTime(year, month, item))
                .Count(date => date.DayOfWeek == dayOfWeek);

            return totalDays;
        }

        public static DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.None)
        {
            var CUSTOM_DATE_FORMATS = new[]
            {
                "yyyyMMddTHHmmssZ",
                "yyyyMMddTHHmmZ",
                "yyyyMMddTHHmmss",
                "yyyyMMddTHHmm",
                "yyyyMMddHHmmss",
                "yyyyMMddHHmm",
                "yyyyMMdd",
                "yyyy-MM-ddTHH-mm-ss",
                "yyyy-MM-dd-HH-mm-ss",
                "yyyy-MM-dd-HH-mm",
                "yyyy-MM-dd",
                "MM-dd-yyyy",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-dd-HH:mm:ss",
                "yyyy-MM-ddTHH:mm:ss zz",
                "yyyy-MM-dd-HH:mm:ss zz",
                "yyyy-MM-dd HH:mm"
            };

            if (formats == null || !formats.Any()) formats = CUSTOM_DATE_FORMATS;

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                    if (DateTime.TryParseExact(dateToParse, format,
                        provider,
                        DateTimeStyles.AssumeUniversal,
                        out validDate))
                        return validDate;

                if (DateTime.TryParseExact(dateToParse, format,
                    provider, styles, out validDate))
                    return validDate;
            }

            return null;
        }

        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}