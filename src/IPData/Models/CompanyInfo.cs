using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class CompanyInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("network")]
        public string Network { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
