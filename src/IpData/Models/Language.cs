using Newtonsoft.Json;

namespace IpData.Models
{
    public class Language
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }
}
