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
    /// <summary>Implementation of <see cref="IIpDataClient"/>.</summary>
    public class IpDataClient : IIpDataClient
    {
        /// <summary>The serializer</summary>
        private static readonly ISerializer _serializer = new JsonSerializer();
        /// <summary>The exception factory</summary>
        private static readonly IApiExceptionFactory _exceptionFactory = new ApiExceptionFactory();

        /// <summary>The HTTP client</summary>
        private readonly IHttpClient _httpClient;

        /// <inheritdoc />
        public string ApiKey { get; }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        public IpDataClient(string apiKey)
            : this(apiKey, new HttpClientAdapter())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public IpDataClient(string apiKey, HttpClient httpClient)
            : this(apiKey, new HttpClientAdapter(httpClient))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentException">The {nameof(apiKey)} {apiKey} - apiKey</exception>
        /// <exception cref="ArgumentNullException">httpClient - The {nameof(httpClient)}</exception>
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

        /// <inheritdoc />
        public Task<IpInfo> Lookup() =>
            Lookup(CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public async Task<IpInfo> Lookup(CultureInfo culture)
        {
            var url = ApiUrls.Get(ApiKey, culture);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        /// <inheritdoc />
        public Task<IpInfo> Lookup(string ip) =>
            Lookup(ip, CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public async Task<IpInfo> Lookup(string ip, CultureInfo culture)
        {
            var url = ApiUrls.Get(ApiKey, ip, culture);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        /// <inheritdoc />
        public Task<string> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector)
        {
            var url = ApiUrls.Get(ApiKey, ip, fieldSelector);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return SendRequestAsync(_httpClient, request);
        }
        
        /// <inheritdoc />
        public async Task<IpInfo> Lookup(string ip, params Expression<Func<IpInfo, object>>[] fieldSelectors)
        {
            var url = ApiUrls.Get(ApiKey, ip, fieldSelectors);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<CarrierInfo> Carrier(string ip)
        {
            var url = ApiUrls.Carrier(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CarrierInfo>(json);
        }

        /// <inheritdoc />
        public async Task<AsnInfo> Asn(string ip)
        {
            var url = ApiUrls.Asn(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<AsnInfo>(json);
        }

        /// <inheritdoc />
        public async Task<Models.TimeZone> TimeZone(string ip)
        {
            var url = ApiUrls.TimeZone(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Models.TimeZone>(json);
        }

        /// <inheritdoc />
        public async Task<Currency> Currency(string ip)
        {
            var url = ApiUrls.Currency(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Currency>(json);
        }

        /// <inheritdoc />
        public async Task<Threat> Threat(string ip)
        {
            var url = ApiUrls.Threat(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Threat>(json);
        }

        /// <summary>Sends the request asynchronous.</summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="request">The request.</param>
        /// <returns>The HTTP request body.</returns>
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
