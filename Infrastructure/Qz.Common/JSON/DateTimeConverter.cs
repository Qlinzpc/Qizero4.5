
namespace Qz.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using JSON = Newtonsoft.Json;

    public class DateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = DateTime.Now;

            if (DateTime.TryParse(reader.Value.ToString(), out date))
            {
                return date;
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = DateTime.Now;

            if (DateTime.TryParse(value.ToString(), out date))
            {
                writer.WriteValue(date.ToString("yyyy-MM-dd hh:mm:ss"));
                return;
            }

            writer.WriteValue(value);
        }
    }
}
