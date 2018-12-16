using Newtonsoft.Json;

namespace IpData.Models
{
    public class Threat
    {
        [JsonProperty("is_tor")]
        public bool IsTor { get; set; }

        [JsonProperty("is_proxy")]
        public bool IsProxy { get; set; }

        [JsonProperty("is_anonymous")]
        public bool IsAnonymous { get; set; }

        [JsonProperty("is_known_attacker")]
        public bool IsKnownAttacker { get; set; }

        [JsonProperty("is_known_abuser")]
        public bool IsKnownAbuser { get; set; }

        [JsonProperty("is_threat")]
        public bool IsThreat { get; set; }

        [JsonProperty("is_bogon")]
        public bool IsBogon { get; set; }
    }
}
