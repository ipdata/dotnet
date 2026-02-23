using Newtonsoft.Json;

namespace IPData.Models
{
    public class Language
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("native", NullValueHandling = NullValueHandling.Ignore)]
        public string Native { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
    }
}
