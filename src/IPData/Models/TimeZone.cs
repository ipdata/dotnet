using System;
using System.Text.Json.Serialization;

namespace IPData.Models
{
    public class TimeZone
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abbr")]
        public string Abbr { get; set; }

        [JsonPropertyName("offset")]
        public string Offset { get; set; }

        [JsonPropertyName("is_dst")]
        public bool? IsDst { get; set; }

        [JsonPropertyName("current_time")]
        public DateTimeOffset? CurrentTime { get; set; }
    }
}
