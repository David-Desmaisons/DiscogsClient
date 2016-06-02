using Newtonsoft.Json;
using System;
using System.Linq;

namespace DiscogsClient.Internal
{
    internal class DiscogsDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                return null;

            var value = (string)reader.Value;
            var values = value.Split('-').Select(int.Parse).ToList();

            switch (values.Count) 
            {
                case 1:
                    return new DateTime(values[0], 1, 1);

                case 2:
                    return new DateTime(values[0], values[1], 1);

                case 3:
                    return new DateTime(values[0], values[1], values[2]);
            }
            return null;
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
