using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class BlocklistInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
