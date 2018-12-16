using Newtonsoft.Json;
using System;

namespace IpData.Models
{
    public class TimeZone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("abbr")]
        public string Abbr { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("is_dst")]
        public bool? IsDst { get; set; }

        [JsonProperty("current_time")]
        public DateTimeOffset? CurrentTime { get; set; }
    }
}
