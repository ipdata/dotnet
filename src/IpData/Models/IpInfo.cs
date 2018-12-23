using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace IpData.Models
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("is_eu")]
        public bool IsEu { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }

        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("asn")]
        public string Asn { get; set; }

        [JsonProperty("organisation")]
        public string Organisation { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }

        [JsonProperty("calling_code")]
        public string CallingCode { get; set; }

        [JsonProperty("flag")]
        public Uri Flag { get; set; }

        [JsonProperty("emoji_flag")]
        public string EmojiFlag { get; set; }

        [JsonProperty("emoji_unicode")]
        public string EmojiUnicode { get; set; }

        [JsonProperty("languages")]
        public List<Language> Languages { get; private set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("time_zone")]
        public TimeZone TimeZone { get; set; }

        [JsonProperty("threat")]
        public Threat Threat { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }

        public IpInfo()
        {
            Languages = new List<Language>();
        }
    }
}