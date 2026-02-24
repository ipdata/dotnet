using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using IPData.Helpers.Extensions;
using IPData.Http.Serializer;

namespace IPData.Models
{
    public class IPLookupResult
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("is_eu")]
        public bool? IsEu { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("continent_name")]
        public string ContinentName { get; set; }

        [JsonPropertyName("continent_code")]
        public string ContinentCode { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("asn")]
        public AsnInfo Asn { get; set; }

        [JsonPropertyName("organisation")]
        public string Organisation { get; set; }

        [JsonPropertyName("postal")]
        public string Postal { get; set; }

        [JsonPropertyName("calling_code")]
        public string CallingCode { get; set; }

        [JsonPropertyName("flag")]
        public Uri Flag { get; set; }

        [JsonPropertyName("emoji_flag")]
        public string EmojiFlag { get; set; }

        [JsonPropertyName("emoji_unicode")]
        public string EmojiUnicode { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; private set; } = new List<Language>();

        [JsonPropertyName("currency")]
        public Currency Currency { get; set; }

        [JsonPropertyName("time_zone")]
        public TimeZone TimeZone { get; set; }

        [JsonPropertyName("threat")]
        public Threat Threat { get; set; }

        [JsonPropertyName("region_type")]
        public string RegionType { get; set; }

        [JsonPropertyName("carrier")]
        public CarrierInfo Carrier { get; set; }

        [JsonPropertyName("company")]
        public CompanyInfo Company { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("count")]
        [JsonConverter(typeof(IntJsonConverter))]
        public int Count { get; set; }

        internal static string FieldName(Expression<Func<IPLookupResult, object>> expression)
        {
            var propName = expression.PropertyName();
            var attribute = typeof(IPLookupResult)
                .GetProperty(propName)
                ?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .Single() as JsonPropertyNameAttribute;

            return attribute?.Name ?? string.Empty;
        }
    }
}
