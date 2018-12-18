using System.Net.Http;
using System.Threading.Tasks;

namespace IpData
{
    interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
