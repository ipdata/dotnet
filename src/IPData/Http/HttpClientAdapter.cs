using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IPData
{
    /// <summary>
    /// Adapts an <see cref="HttpClient"/> to <see cref="IHttpClient"/>. Adapters created without an
    /// <see cref="HttpClient"/> share one process-wide client, so every <see cref="IPDataClient"/> built that
    /// way reuses the same pooled HTTPS connections for the lifetime of the process.
    /// </summary>
    internal class HttpClientAdapter : IHttpClient
    {
        /// <summary>
        /// The process-wide default client. It is created on first use and intentionally never disposed:
        /// disposing it would tear down the shared connection pool.
        /// </summary>
        private static readonly Lazy<HttpClient> _defaultClient =
            new Lazy<HttpClient>(CreateDefaultClient, LazyThreadSafetyMode.ExecutionAndPublication);

        private readonly HttpClient _httpClient;

        /// <summary>Creates an adapter over the shared default <see cref="HttpClient"/>.</summary>
        public HttpClientAdapter()
            : this(_defaultClient.Value)
        {
        }

        /// <summary>Creates an adapter over a caller-owned <see cref="HttpClient"/>; the caller controls its lifetime.</summary>
        /// <param name="httpClient">The HTTP client.</param>
        public HttpClientAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(
                nameof(httpClient),
                $"The {nameof(httpClient)} can't be null");
        }

        /// <summary>The <see cref="HttpClient"/> that requests are sent through.</summary>
        internal HttpClient Client => _httpClient;

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) =>
            _httpClient.SendAsync(request);

        /// <summary>Creates the default <see cref="HttpClient"/> over the handler from <see cref="CreateDefaultHandler"/>.</summary>
        internal static HttpClient CreateDefaultClient() =>
            new HttpClient(CreateDefaultHandler());

        /// <summary>
        /// Creates the default message handler. On .NET 5 and later it is a <see cref="SocketsHttpHandler"/>
        /// that recycles connections after 5 minutes, closes idle ones after 60 seconds and keeps HTTP/2
        /// connections alive with a ping every 30 seconds; on .NET Standard 2.0 it is a plain
        /// <see cref="HttpClientHandler"/>. Cookies are disabled in both cases because the API is stateless.
        /// </summary>
        internal static HttpMessageHandler CreateDefaultHandler()
        {
#if NET5_0_OR_GREATER
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(60),
                KeepAlivePingDelay = TimeSpan.FromSeconds(30),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(15),
                KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always,
                UseCookies = false
            };
#else
            return new HttpClientHandler
            {
                UseCookies = false
            };
#endif
        }
    }
}
