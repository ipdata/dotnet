using System.Text.Json;
using System.Text.Json.Serialization;

namespace IPData.Http.Serializer
{
    internal class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
#if NET5_0_OR_GREATER
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#else
            IgnoreNullValues = true
#endif
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
