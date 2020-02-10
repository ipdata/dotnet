using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IpData.Helpers.Extensions;
using Newtonsoft.Json;

namespace IpData.Models
{
    public class IpInfo
    {
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        [JsonProperty("is_eu", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEu { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty("region_code", NullValueHandling = NullValueHandling.Ignore)]
        public string RegionCode { get; set; }

        [JsonProperty("country_name", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryName { get; set; }

        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        [JsonProperty("continent_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ContinentName { get; set; }

        [JsonProperty("continent_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContinentCode { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitude { get; set; }

        [JsonProperty("asn", NullValueHandling = NullValueHandling.Ignore)]
        public AsnInfo Asn { get; set; }

        [JsonProperty("organisation", NullValueHandling = NullValueHandling.Ignore)]
        public string Organisation { get; set; }

        [JsonProperty("postal", NullValueHandling = NullValueHandling.Ignore)]
        public string Postal { get; set; }

        [JsonProperty("calling_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CallingCode { get; set; }

        [JsonProperty("flag", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Flag { get; set; }

        [JsonProperty("emoji_flag", NullValueHandling = NullValueHandling.Ignore)]
        public string EmojiFlag { get; set; }

        [JsonProperty("emoji_unicode", NullValueHandling = NullValueHandling.Ignore)]
        public string EmojiUnicode { get; set; }

        [JsonProperty("languages")]
        public List<Language> Languages { get; private set; } = new List<Language>();

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("time_zone")]
        public TimeZone TimeZone { get; set; }

        [JsonProperty("threat")]
        public Threat Threat { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        internal static string FieldName(Expression<Func<IpInfo, object>> expression)
        {
            var propName = expression.PropertyName();
            var attribute = typeof(IpInfo)
                .GetProperty(propName)
                ?.GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                .Single() as JsonPropertyAttribute;

            return attribute?.PropertyName ?? string.Empty;
        }
    }
}