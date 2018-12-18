using System.Net.Http;
using System.Threading.Tasks;

namespace IpData
{
    internal class HttpClientAdapter : IHttpClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _httpClient.SendAsync(request);
        }
    }
}
