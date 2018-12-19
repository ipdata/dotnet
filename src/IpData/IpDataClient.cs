using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IpData.Exceptions.Factory;
using IpData.Helpers;
using IpData.Http.Serializer;
using IpData.Models;

namespace IpData
{
    public class IpDataClient : IIpDataClient
    {
        private static readonly ISerializer _serializer = new JsonSerializer();
        private static readonly IHttpClient _httpClient = new DefaultHttpClient();
        private static readonly IApiExceptionFactory _exceptionFactory = new ApiExceptionFactory();

        public string ApiKey { get; private set; }

        public IpDataClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException(
                    $"The apiKey {apiKey} must be not empty or whitespace string",
                    nameof(apiKey));
            }

            ApiKey = apiKey;
        }

        public Task<IpInfo> Lookup()
        {
            return Lookup(CultureInfo.InvariantCulture);
        }

        public async Task<IpInfo> Lookup(CultureInfo culture)
        {
            var url = ApiUrls.Get(ApiKey, culture);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequest(request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        public Task<IpInfo> Lookup(string ip)
        {
            return Lookup(ip, CultureInfo.InvariantCulture);
        }

        public async Task<IpInfo> Lookup(string ip, CultureInfo culture)
        {
            var url = ApiUrls.Get(ApiKey, ip, culture);
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
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            throw _exceptionFactory.Create(response.StatusCode, content);
        }
    }
}
