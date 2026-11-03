using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPData.Tests.Http
{
    /// <summary>
    /// Records every request it receives and answers each one with a fresh 200 OK JSON response,
    /// so tests can drive a real <see cref="HttpClient"/> without touching the network.
    /// </summary>
    public sealed class RecordingHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseBody;

        public RecordingHttpMessageHandler(string responseBody = "{}")
        {
            _responseBody = responseBody;
        }

        public List<HttpRequestMessage> Requests { get; } = new List<HttpRequestMessage>();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = request,
                Content = new StringContent(_responseBody, Encoding.UTF8, "application/json")
            });
        }
    }
}
