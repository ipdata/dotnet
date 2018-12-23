using System.Net.Http;
using System.Threading.Tasks;

namespace IpData
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
