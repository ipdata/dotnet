using Newtonsoft.Json;

namespace IpData.Models
{
    public class CarrierInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mcc")]
        public string Mcc { get; set; }

        [JsonProperty("mnc")]
        public string Mnc { get; set; }
    }

}
