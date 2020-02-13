using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IpData
{
    internal class HttpClientAdapter : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientAdapter()
            : this(new HttpClient())
        {
        }

        public HttpClientAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(
                nameof(httpClient),
                $"The {nameof(httpClient)} can't be null");
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) =>
            _httpClient.SendAsync(request);
    }
}
