using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IpData.Exceptions.Factory;
using IpData.Helpers;
using IpData.Http.Serializer;
using IpData.Models;

[assembly: InternalsVisibleTo("IpData.Tests")]

namespace IpData
{
    public class IpDataClient : IIpDataClient
    {
        private static readonly ISerializer _serializer = new JsonSerializer();
        private static readonly IApiExceptionFactory _exceptionFactory = new ApiExceptionFactory();

        private readonly IHttpClient _httpClient;

        public string ApiKey { get; private set; }

        public IpDataClient(string apiKey)
            : this(apiKey, new HttpClientAdapter())
        {
        }

        public IpDataClient(string apiKey, HttpClient httpClient)
            : this(apiKey, new HttpClientAdapter(httpClient))
        {
        }

        public IpDataClient(string apiKey, IHttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException(
                    $"The {nameof(apiKey)} {apiKey} must be not empty or whitespace string",
                    nameof(apiKey));
            }

            _httpClient = httpClient ?? throw new ArgumentNullException(
                nameof(httpClient),
                $"The {nameof(httpClient)} can't be null");

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
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
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
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        public Task<string> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector)
        {
            var url = ApiUrls.Get(ApiKey, ip, fieldSelector);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return SendRequestAsync(_httpClient, request);
        }

        public async Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips)
        {
            var url = ApiUrls.Bulk(ApiKey);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(_serializer.Serialize(ips), Encoding.UTF8, "application/json")
            };

            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IEnumerable<IpInfo>>(json);
        }

        public async Task<CarrierInfo> Carrier(string ip)
        {
            var url = ApiUrls.Carrier(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CarrierInfo>(json);
        }

        private static async Task<string> SendRequestAsync(IHttpClient httpClient, HttpRequestMessage request)
        {
            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            var content = string.Empty;

            if (response.Content != null)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            throw _exceptionFactory.Create(response.StatusCode, content);
        }
    }
}
