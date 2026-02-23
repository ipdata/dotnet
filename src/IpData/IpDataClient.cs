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

        /// <summary>The API URL builder</summary>
        private readonly ApiUrls _apiUrls;

        /// <inheritdoc />
        public string ApiKey { get; }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        public IpDataClient(string apiKey)
            : this(apiKey, new HttpClientAdapter(), null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public IpDataClient(string apiKey, HttpClient httpClient)
            : this(apiKey, new HttpClientAdapter(httpClient), null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public IpDataClient(string apiKey, IHttpClient httpClient)
            : this(apiKey, httpClient, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class with a custom base URL.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="baseUrl">The base URL (e.g. https://eu-api.ipdata.co for the EU endpoint).</param>
        public IpDataClient(string apiKey, Uri baseUrl)
            : this(apiKey, new HttpClientAdapter(), baseUrl)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IpDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="baseUrl">The base URL. Defaults to https://api.ipdata.co when null.</param>
        /// <exception cref="ArgumentException">The {nameof(apiKey)} {apiKey} - apiKey</exception>
        /// <exception cref="ArgumentNullException">httpClient - The {nameof(httpClient)}</exception>
        public IpDataClient(string apiKey, IHttpClient httpClient, Uri baseUrl)
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
            _apiUrls = new ApiUrls(baseUrl);
        }

        /// <inheritdoc />
        public Task<IpInfo> Lookup() =>
            Lookup(CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public async Task<IpInfo> Lookup(CultureInfo culture)
        {
            var url = _apiUrls.Get(ApiKey, culture);
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
            var url = _apiUrls.Get(ApiKey, ip, culture);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        /// <inheritdoc />
        public Task<string> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector)
        {
            var url = _apiUrls.Get(ApiKey, ip, fieldSelector);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return SendRequestAsync(_httpClient, request);
        }
        
        /// <inheritdoc />
        public async Task<IpInfo> Lookup(string ip, params Expression<Func<IpInfo, object>>[] fieldSelectors)
        {
            var url = _apiUrls.Get(ApiKey, ip, fieldSelectors);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IpInfo>(json);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips)
        {
            var url = _apiUrls.Bulk(ApiKey);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(_serializer.Serialize(ips), Encoding.UTF8, "application/json")
            };

            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IEnumerable<IpInfo>>(json);
        }

        /// <inheritdoc />
        public async Task<CompanyInfo> Company(string ip)
        {
            var url = _apiUrls.Company(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CompanyInfo>(json);
        }

        /// <inheritdoc />
        public async Task<CarrierInfo> Carrier(string ip)
        {
            var url = _apiUrls.Carrier(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CarrierInfo>(json);
        }

        /// <inheritdoc />
        public async Task<AsnInfo> Asn(string ip)
        {
            var url = _apiUrls.Asn(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<AsnInfo>(json);
        }

        /// <inheritdoc />
        public async Task<Models.TimeZone> TimeZone(string ip)
        {
            var url = _apiUrls.TimeZone(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Models.TimeZone>(json);
        }

        /// <inheritdoc />
        public async Task<Currency> Currency(string ip)
        {
            var url = _apiUrls.Currency(ApiKey, ip);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Currency>(json);
        }

        /// <inheritdoc />
        public async Task<Threat> Threat(string ip)
        {
            var url = _apiUrls.Threat(ApiKey, ip);
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
