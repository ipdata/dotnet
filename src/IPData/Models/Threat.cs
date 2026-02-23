using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class Threat
    {
        [JsonPropertyName("is_tor")]
        public bool? IsTor { get; set; }

        [JsonPropertyName("is_proxy")]
        public bool? IsProxy { get; set; }

        [JsonPropertyName("is_anonymous")]
        public bool? IsAnonymous { get; set; }

        [JsonPropertyName("is_known_attacker")]
        public bool? IsKnownAttacker { get; set; }

        [JsonPropertyName("is_known_abuser")]
        public bool? IsKnownAbuser { get; set; }

        [JsonPropertyName("is_threat")]
        public bool? IsThreat { get; set; }

        [JsonPropertyName("is_bogon")]
        public bool? IsBogon { get; set; }

        [JsonPropertyName("is_icloud_relay")]
        public bool? IsIcloudRelay { get; set; }

        [JsonPropertyName("is_datacenter")]
        public bool? IsDatacenter { get; set; }

        [JsonPropertyName("blocklists")]
        public List<BlocklistInfo> Blocklists { get; private set; } = new List<BlocklistInfo>();
    }
}
