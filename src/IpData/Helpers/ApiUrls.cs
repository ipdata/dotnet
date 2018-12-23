using System;
using System.Globalization;
using System.Linq.Expressions;
using IpData.Helpers.Extensions;
using IpData.Models;
using Newtonsoft.Json;

namespace IpData.Helpers
{
    internal static class ApiUrls
    {
        public static Uri Base => new Uri($"https://api.ipdata.co");

        public static Uri Get(string apiKey, CultureInfo culture)
        {
            return ApplyApiKey(new Uri(Base, $"{culture}"), apiKey);
        }

        public static Uri Get(string apiKey, string ip, CultureInfo culture)
        {
            var relative = culture == CultureInfo.InvariantCulture ? ip : $"{ip}/{culture}";
            return ApplyApiKey(new Uri(Base, relative), apiKey);
        }

        public static Uri Get(string apiKey, string ip, Expression<Func<IpInfo, object>> expression)
        {
            var field = IpInfo.FieldName(expression);
            return ApplyApiKey(new Uri(Base, $"{ip}/{field}"), apiKey);
        }

        public static Uri Bulk(string apiKey)
        {
            return ApplyApiKey(new Uri(Base, "bulk"), apiKey);
        }

        public static Uri Carrier(string apiKey, string ip)
        {
            return ApplyApiKey(new Uri(Base, $"{ip}/carrier"), apiKey);
        }

        private static Uri ApplyApiKey(Uri url, string apiKey)
        {
            return url.AddParameter("api-key", apiKey);
        }
    }
}
