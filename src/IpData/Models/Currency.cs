using Newtonsoft.Json;

namespace IpData.Models
{
    public class Currency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }

        [JsonProperty("plural")]
        public string Plural { get; set; }
    }
}
