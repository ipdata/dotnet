using Newtonsoft.Json;

namespace IpData.Models
{
    public class Currency
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("native", NullValueHandling = NullValueHandling.Ignore)]
        public string Native { get; set; }

        [JsonProperty("plural", NullValueHandling = NullValueHandling.Ignore)]
        public string Plural { get; set; }
    }
}
