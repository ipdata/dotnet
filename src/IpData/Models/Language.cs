using Newtonsoft.Json;

namespace IpData.Models
{
    public class Language
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("native", NullValueHandling = NullValueHandling.Ignore)]
        public string Native { get; set; }
    }
}
