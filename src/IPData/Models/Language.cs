using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("native")]
        public string Native { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
