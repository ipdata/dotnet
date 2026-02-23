using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class Currency
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("native")]
        public string Native { get; set; }

        [JsonPropertyName("plural")]
        public string Plural { get; set; }
    }
}
