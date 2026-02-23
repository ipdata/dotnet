using System.Net.Http;
using System.Threading.Tasks;

namespace IPData
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
