using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IPData.Exceptions.Factory;
using IPData.Helpers;
using IPData.Http.Serializer;
using IPData.Models;

[assembly: InternalsVisibleTo("IPData.Tests")]

namespace IPData
{
    /// <summary>Implementation of <see cref="IIPDataClient"/>.</summary>
    /// <remarks>
    /// Create one instance per process (or one per API key or endpoint) and reuse it for every lookup: the
    /// instance keeps its HTTPS connections pooled and reuses them across requests instead of paying a TLS
    /// handshake per call. Instances created without an <see cref="HttpClient"/> share one process-wide
    /// client that lives as long as the process does and is never disposed; an <see cref="HttpClient"/> or
    /// <see cref="IHttpClient"/> passed in stays owned by the caller. The class holds no per-request state
    /// and is safe to use from several threads at once. On .NET 5 and later every request asks for HTTP/2
    /// and falls back to HTTP/1.1 when the server does not negotiate it. Every request carries a
    /// <c>User-Agent</c> header naming the library and its version and an <c>Accept: application/json</c> header.
    /// </remarks>
    public class IPDataClient : IIPDataClient
    {
        /// <summary>The serializer</summary>
        private static readonly ISerializer _serializer = new JsonSerializer();
        /// <summary>The exception factory</summary>
        private static readonly IApiExceptionFactory _exceptionFactory = new ApiExceptionFactory();

        /// <summary>The product token sent in the <c>User-Agent</c> header of every request.</summary>
        internal const string UserAgentProduct = "ipdata-dotnet";

        /// <summary>
        /// The <c>User-Agent</c> value sent with every request, <c>ipdata-dotnet/&lt;version&gt;</c>, where the
        /// version is the package version of this assembly. It is read once, when the type is initialized.
        /// </summary>
        internal static readonly ProductInfoHeaderValue UserAgent = CreateUserAgent();

        /// <summary>The HTTP client</summary>
        private readonly IHttpClient _httpClient;

        /// <summary>The API URL builder</summary>
        private readonly ApiUrls _apiUrls;

        /// <inheritdoc />
        public string ApiKey { get; }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        public IPDataClient(string apiKey)
            : this(apiKey, new HttpClientAdapter(), null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public IPDataClient(string apiKey, HttpClient httpClient)
            : this(apiKey, new HttpClientAdapter(httpClient), null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public IPDataClient(string apiKey, IHttpClient httpClient)
            : this(apiKey, httpClient, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class with a custom base URL.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="baseUrl">The base URL (e.g. https://eu-api.ipdata.co for the EU endpoint).</param>
        public IPDataClient(string apiKey, Uri baseUrl)
            : this(apiKey, new HttpClientAdapter(), baseUrl)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class with a caller-owned HTTP client and a custom base URL.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client. The caller owns it and controls its lifetime.</param>
        /// <param name="baseUrl">The base URL (e.g. https://eu-api.ipdata.co for the EU endpoint). Defaults to https://api.ipdata.co when null.</param>
        /// <exception cref="ArgumentNullException">httpClient - The {nameof(httpClient)}</exception>
        public IPDataClient(string apiKey, HttpClient httpClient, Uri baseUrl)
            : this(apiKey, new HttpClientAdapter(httpClient), baseUrl)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IPDataClient"/> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="baseUrl">The base URL. Defaults to https://api.ipdata.co when null.</param>
        /// <exception cref="ArgumentException">The {nameof(apiKey)} {apiKey} - apiKey</exception>
        /// <exception cref="ArgumentNullException">httpClient - The {nameof(httpClient)}</exception>
        public IPDataClient(string apiKey, IHttpClient httpClient, Uri baseUrl)
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
        public Task<IPLookupResult> Lookup() =>
            Lookup(CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public async Task<IPLookupResult> Lookup(CultureInfo culture)
        {
            var url = _apiUrls.Get(ApiKey, culture);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IPLookupResult>(json);
        }

        /// <inheritdoc />
        public Task<IPLookupResult> Lookup(string ip) =>
            Lookup(ip, CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public async Task<IPLookupResult> Lookup(string ip, CultureInfo culture)
        {
            var url = _apiUrls.Get(ApiKey, ip, culture);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IPLookupResult>(json);
        }

        /// <inheritdoc />
        public Task<string> Lookup(string ip, Expression<Func<IPLookupResult, object>> fieldSelector)
        {
            var url = _apiUrls.Get(ApiKey, ip, fieldSelector);
            var request = CreateRequest(HttpMethod.Get, url);
            return SendRequestAsync(_httpClient, request);
        }
        
        /// <inheritdoc />
        public async Task<IPLookupResult> Lookup(string ip, params Expression<Func<IPLookupResult, object>>[] fieldSelectors)
        {
            var url = _apiUrls.Get(ApiKey, ip, fieldSelectors);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IPLookupResult>(json);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IPLookupResult>> Lookup(IReadOnlyCollection<string> ips)
        {
            var url = _apiUrls.Bulk(ApiKey);
            var request = CreateRequest(HttpMethod.Post, url);
            request.Content = new StringContent(_serializer.Serialize(ips), Encoding.UTF8, "application/json");

            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<IEnumerable<IPLookupResult>>(json);
        }

        /// <inheritdoc />
        public async Task<CompanyInfo> Company(string ip)
        {
            var url = _apiUrls.Company(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CompanyInfo>(json);
        }

        /// <inheritdoc />
        public async Task<CarrierInfo> Carrier(string ip)
        {
            var url = _apiUrls.Carrier(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<CarrierInfo>(json);
        }

        /// <inheritdoc />
        public async Task<AsnInfo> Asn(string ip)
        {
            var url = _apiUrls.Asn(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<AsnInfo>(json);
        }

        /// <inheritdoc />
        public async Task<Models.TimeZone> TimeZone(string ip)
        {
            var url = _apiUrls.TimeZone(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Models.TimeZone>(json);
        }

        /// <inheritdoc />
        public async Task<Currency> Currency(string ip)
        {
            var url = _apiUrls.Currency(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Currency>(json);
        }

        /// <inheritdoc />
        public async Task<Threat> Threat(string ip)
        {
            var url = _apiUrls.Threat(ApiKey, ip);
            var request = CreateRequest(HttpMethod.Get, url);
            var json = await SendRequestAsync(_httpClient, request).ConfigureAwait(false);
            return _serializer.Deserialize<Threat>(json);
        }

        /// <summary>Creates the request message for an API call.</summary>
        /// <remarks>
        /// Every request carries the library's <c>User-Agent</c> and an <c>Accept: application/json</c> header.
        /// On .NET 5 and later the request asks for HTTP/2 and lets the handler fall back to HTTP/1.1
        /// (HttpVersionPolicy.RequestVersionOrLower). The .NET Standard 2.0 build leaves the default version
        /// alone, because .NET Framework rejects requests that ask for HTTP/2.
        /// </remarks>
        /// <param name="method">The HTTP method.</param>
        /// <param name="url">The request URL.</param>
        /// <returns>The request message.</returns>
        private static HttpRequestMessage CreateRequest(HttpMethod method, Uri url)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.UserAgent.Add(UserAgent);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
#if NET5_0_OR_GREATER
            request.Version = System.Net.HttpVersion.Version20;
            request.VersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
#endif
            return request;
        }

        /// <summary>
        /// Builds the <c>User-Agent</c> value from the assembly's informational version, which is the package
        /// version without the <c>+commit</c> build metadata the SDK appends to it. Falls back to the assembly
        /// version when the informational version is missing or is not a valid header token.
        /// </summary>
        /// <returns>The <c>ipdata-dotnet/&lt;version&gt;</c> product token.</returns>
        private static ProductInfoHeaderValue CreateUserAgent()
        {
            var assembly = typeof(IPDataClient).Assembly;
            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            if (!string.IsNullOrWhiteSpace(version))
            {
                var metadataStart = version.IndexOf('+');
                if (metadataStart >= 0)
                {
                    version = version.Substring(0, metadataStart);
                }

                if (ProductInfoHeaderValue.TryParse($"{UserAgentProduct}/{version}", out var userAgent))
                {
                    return userAgent;
                }
            }

            return new ProductInfoHeaderValue(UserAgentProduct, assembly.GetName().Version?.ToString(3) ?? "0.0.0");
        }

        /// <summary>Sends the request asynchronous and disposes the request and the response once the body has been read.</summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="request">The request.</param>
        /// <returns>The HTTP response body.</returns>
        private static async Task<string> SendRequestAsync(IHttpClient httpClient, HttpRequestMessage request)
        {
            using (request)
            using (var response = await httpClient.SendAsync(request).ConfigureAwait(false))
            {
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
}
