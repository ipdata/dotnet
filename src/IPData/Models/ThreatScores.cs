using System.Text.Json.Serialization;

namespace IPData.Models
{
    /// <summary>
    /// The <c>scores</c> object of a threat response. Each score is <c>null</c> when the response does not
    /// include it.
    /// </summary>
    public class ThreatScores
    {
        [JsonPropertyName("vpn_score")]
        public int? VpnScore { get; set; }

        [JsonPropertyName("proxy_score")]
        public int? ProxyScore { get; set; }

        [JsonPropertyName("threat_score")]
        public int? ThreatScore { get; set; }

        [JsonPropertyName("trust_score")]
        public int? TrustScore { get; set; }
    }
}
