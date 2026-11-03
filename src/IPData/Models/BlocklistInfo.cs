using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class BlocklistInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// First day the address was seen on this blocklist, as a <c>YYYY-MM-DD</c> (UTC) string,
        /// or <c>null</c> when the feed does not report it.
        /// </summary>
        [JsonPropertyName("first_seen")]
        public string FirstSeen { get; set; }

        /// <summary>
        /// Last day the address was seen on this blocklist, as a <c>YYYY-MM-DD</c> (UTC) string,
        /// or <c>null</c> when the feed does not report it.
        /// </summary>
        [JsonPropertyName("last_seen")]
        public string LastSeen { get; set; }

        /// <summary>
        /// Number of days the address was seen on this blocklist, or <c>null</c> when the feed does
        /// not report it.
        /// </summary>
        [JsonPropertyName("days_seen")]
        public int? DaysSeen { get; set; }
    }
}
