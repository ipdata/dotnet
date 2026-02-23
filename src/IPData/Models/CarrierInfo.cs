using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class CarrierInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mcc")]
        public string Mcc { get; set; }

        [JsonPropertyName("mnc")]
        public string Mnc { get; set; }
    }
}
