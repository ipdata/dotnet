using Newtonsoft.Json;

namespace IpData.Models
{
    public class CarrierInfo
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("mcc", NullValueHandling = NullValueHandling.Ignore)]
        public string Mcc { get; set; }

        [JsonProperty("mnc", NullValueHandling = NullValueHandling.Ignore)]
        public string Mnc { get; set; }
    }

}
