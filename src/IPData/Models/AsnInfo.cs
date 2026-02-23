using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class AsnInfo
    {
        [JsonPropertyName("asn")]
        public string Asn { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("route")]
        public string Route { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
