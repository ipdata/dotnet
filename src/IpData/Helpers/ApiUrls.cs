using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using IpData.Helpers.Extensions;
using IpData.Models;

namespace IpData.Helpers
{
    internal class ApiUrls
    {
        internal static readonly Uri DefaultBaseUrl = new Uri("https://api.ipdata.co");

        private readonly Uri _base;

        public ApiUrls(Uri baseUrl = null)
        {
            _base = baseUrl ?? DefaultBaseUrl;
        }

        public Uri Get(string apiKey, CultureInfo culture) =>
            ApplyApiKey(new Uri(_base, $"{culture}"), apiKey);

        public Uri Get(string apiKey, string ip, CultureInfo culture)
        {
            var relative = Equals(culture, CultureInfo.InvariantCulture) ? ip : $"{ip}/{culture}";
            return ApplyApiKey(new Uri(_base, relative), apiKey);
        }

        public Uri Get(string apiKey, string ip, Expression<Func<IpInfo, object>> expression)
        {
            var field = IpInfo.FieldName(expression);
            return ApplyApiKey(new Uri(_base, $"{ip}/{field}"), apiKey);
        }

        public Uri Get(string apiKey, string ip, params Expression<Func<IpInfo, object>>[] expressions)
        {
            var fields = string.Join(",", expressions.Select(IpInfo.FieldName));
            return ApplyApiKey(new Uri(_base, $"{ip}").AddParameter(nameof(fields), fields), apiKey);
        }

        public Uri Bulk(string apiKey) =>
            ApplyApiKey(new Uri(_base, "bulk"), apiKey);

        public Uri Carrier(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"{ip}/carrier"), apiKey);

        public Uri Asn(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"asn/{ip}"), apiKey);

        public Uri TimeZone(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"{ip}/time_zone"), apiKey);

        public Uri Currency(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"{ip}/currency"), apiKey);

        public Uri Threat(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"{ip}/threat"), apiKey);

        public Uri Company(string apiKey, string ip) =>
            ApplyApiKey(new Uri(_base, $"{ip}/company"), apiKey);

        private static Uri ApplyApiKey(Uri url, string apiKey) =>
            url.AddParameter("api-key", apiKey);
    }
}
