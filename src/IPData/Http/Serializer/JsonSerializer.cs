using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IPData.Http.Serializer
{
    internal class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            }
        };

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public string Serialize(object item)
        {
            return JsonConvert.SerializeObject(item, _settings);
        }
    }
}
