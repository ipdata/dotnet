using System.Text.Json;

namespace IPData.Http.Serializer
{
    internal class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };

        public T Deserialize<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
        }

        public string Serialize(object item)
        {
            return System.Text.Json.JsonSerializer.Serialize(item, _options);
        }
    }
}
