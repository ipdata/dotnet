using System.Collections.Generic;
using Newtonsoft.Json;

namespace IPData.Models
{
    public class Threat
    {
        [JsonProperty("is_tor", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsTor { get; set; }

        [JsonProperty("is_proxy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsProxy { get; set; }

        [JsonProperty("is_anonymous", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsAnonymous { get; set; }

        [JsonProperty("is_known_attacker", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsKnownAttacker { get; set; }

        [JsonProperty("is_known_abuser", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsKnownAbuser { get; set; }

        [JsonProperty("is_threat", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsThreat { get; set; }

        [JsonProperty("is_bogon", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBogon { get; set; }

        [JsonProperty("is_icloud_relay", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsIcloudRelay { get; set; }

        [JsonProperty("is_datacenter", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDatacenter { get; set; }

        [JsonProperty("blocklists")]
        public List<BlocklistInfo> Blocklists { get; private set; } = new List<BlocklistInfo>();
    }
}
