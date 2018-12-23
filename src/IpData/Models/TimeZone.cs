using System;
using Newtonsoft.Json;

namespace IpData.Models
{
    public class TimeZone
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("abbr", NullValueHandling = NullValueHandling.Ignore)]
        public string Abbr { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string Offset { get; set; }

        [JsonProperty("is_dst", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDst { get; set; }

        [JsonProperty("current_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CurrentTime { get; set; }
    }
}
