using Newtonsoft.Json;

namespace IPData.Models
{
    public class AsnInfo
    {
        [JsonProperty("asn", NullValueHandling = NullValueHandling.Ignore)]
        public string Asn { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
        public string Domain { get; set; }

        [JsonProperty("route", NullValueHandling = NullValueHandling.Ignore)]
        public string Route { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
