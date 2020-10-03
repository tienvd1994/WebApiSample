using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace S0D.Infrastructure.Converters
{
    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        private readonly string[] formats =
        {
            "yyyy-MM-dd",
            "yyyy-MM-dd h:mm:ss",
            "yyyy-MM-dd h:mm:ss",
            "yyyy-MM-dd hh:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-M-d",
            "yyyy-M-d h:mm:ss",
            "yyyy-M-d h:mm:ss",
            "yyyy-M-d hh:mm:ss",
            "yyyy-M-d HH:mm:ss",
            "yyyy-M-d HH:mm:ss",
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-ddThh:mm:sszzz"
        };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            try
            {
                var cultureInfo = new CultureInfo("en-US");
                if (!DateTime.TryParseExact(dateString, formats, cultureInfo, DateTimeStyles.None,
                    out var dateTime))
                    throw new JsonException("Invalid datetime format.");
                return dateTime;
            }
            catch (FormatException)
            {
                throw new JsonException("Invalid datetime format.");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(new CultureInfo("en-US")));
        }
    }
}