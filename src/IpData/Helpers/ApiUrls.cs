using IpData.Helpers.Extensions;
using IpData.Models;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IpData.Tests")]

namespace IpData.Helpers
{
    internal static class ApiUrls
    {
        public static Uri Base => new Uri($"https://api.ipdata.co");

        public static Uri Get(string apiKey)
        {
            return ApplyApiKey(Base, apiKey);
        }

        public static Uri Get(string apiKey, string ip)
        {
            return ApplyApiKey(new Uri(Base, ip), apiKey);
        }

        public static Uri Get(string apiKey, string ip, Expression<Func<IpInfo, object>> expression)
        {
            var field = GetField(expression);
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

        private static string GetField(Expression<Func<IpInfo, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            var propName = body.Member.Name;
            var attribute = typeof(IpInfo)
                .GetProperty(propName)
                .GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;

            return attribute.PropertyName;
        }
    }
}
