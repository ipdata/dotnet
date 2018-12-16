using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IpData.Helpers;
using IpData.Http.Serializer;
using IpData.Models;

namespace IpData
{
    public class IpDataClient : IIpDataClient
    {
        private static readonly ISerializer _serializer = new JsonSerializer();
        private static readonly HttpClient _httpClient = new HttpClient();

        public string ApiKey { get; private set; }

        public string Culture { get; private set; }

        public IpDataClient(string apiKey) : this(apiKey, "en")
        { }

        public IpDataClient(string apiKey, string culture)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException(
                    $"The apiKey {apiKey} must be not empty or whitespace string",
                    nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(culture))
            {
                throw new ArgumentException(
                    $"The culture {culture} must be not empty or whitespace string",
                    nameof(culture));
            }

            ApiKey = apiKey;
            Culture = culture;

            _httpClient.BaseAddress = ApiUrls.Base;
        }

        public async Task<IpInfo> Lookup()
        {
            var url = ApiUrls.Get(ApiKey);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        public async Task<IpInfo> Lookup(string ip)
        {
            var url = ApiUrls.Get(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        public async Task<IpInfo> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector)
        {
            var url = ApiUrls.Get(ApiKey, ip, fieldSelector);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        public async Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips)
        {
            var url = ApiUrls.Bulk(ApiKey);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(_serializer.Serialize(ips), Encoding.UTF8, "application/json")
            };

            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<IEnumerable<IpInfo>>(json);
        }

        public async Task<CarrierInfo> Carrier(string ip)
        {
            var url = ApiUrls.Carrier(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<CarrierInfo>(json);
        }

        private static async Task<string> SendRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
