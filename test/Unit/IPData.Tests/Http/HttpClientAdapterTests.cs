using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace IPData.Tests.Http
{
    public class HttpClientAdapterTests
    {
        [Fact]
        public void HttpClientAdapter_WhenCreatedWithoutHttpClient_ShouldShareOneHttpClientBetweenInstances()
        {
            // Act
            var first = new HttpClientAdapter();
            var second = new HttpClientAdapter();

            // Assert
            first.Client.Should().NotBeNull();
            second.Client.Should().BeSameAs(first.Client);
        }

        [Fact]
        public void HttpClientAdapter_WhenCreatedWithoutHttpClient_ShouldUseThePooledDefaultHandler()
        {
            // Act
            var sut = new HttpClientAdapter();

            // Assert
            GetHandler(sut.Client).Should().BeOfType<SocketsHttpHandler>()
                .Which.PooledConnectionIdleTimeout.Should().Be(TimeSpan.FromSeconds(60));
        }

        [Fact]
        public void HttpClientAdapter_WhenCreatedWithHttpClient_ShouldUseThatHttpClient()
        {
            // Arrange
            using (var httpClient = new HttpClient())
            {
                // Act
                var sut = new HttpClientAdapter(httpClient);

                // Assert
                sut.Client.Should().BeSameAs(httpClient);
                sut.Client.Should().NotBeSameAs(new HttpClientAdapter().Client);
            }
        }

        [Fact]
        public void HttpClientAdapter_WhenCreateWithInvalidHttpClient_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new HttpClientAdapter(null);

            // Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Fact]
        public async Task SendAsync_WhenCalledRepeatedly_ShouldSendEveryRequestThroughTheSameHttpClient()
        {
            // Arrange
            var handler = new RecordingHttpMessageHandler();
            using (var httpClient = new HttpClient(handler))
            {
                var sut = new HttpClientAdapter(httpClient);

                // Act
                using (var first = await sut.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://api.ipdata.co/8.8.8.8")))
                using (var second = await sut.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://api.ipdata.co/1.1.1.1")))
                {
                    // Assert
                    first.StatusCode.Should().Be(HttpStatusCode.OK);
                    second.StatusCode.Should().Be(HttpStatusCode.OK);
                    handler.Requests.Should().HaveCount(2);
                }
            }
        }

        [Fact]
        public void CreateDefaultHandler_WhenCalled_ShouldConfigurePooledSocketsHttpHandler()
        {
            // Act
            using (var handler = HttpClientAdapter.CreateDefaultHandler())
            {
                // Assert
                var socketsHandler = handler.Should().BeOfType<SocketsHttpHandler>().Which;
                socketsHandler.PooledConnectionLifetime.Should().Be(TimeSpan.FromMinutes(5));
                socketsHandler.PooledConnectionIdleTimeout.Should().Be(TimeSpan.FromSeconds(60));
                socketsHandler.KeepAlivePingDelay.Should().Be(TimeSpan.FromSeconds(30));
                socketsHandler.KeepAlivePingTimeout.Should().Be(TimeSpan.FromSeconds(15));
                socketsHandler.KeepAlivePingPolicy.Should().Be(HttpKeepAlivePingPolicy.Always);
                socketsHandler.UseCookies.Should().BeFalse();
            }
        }

        [Fact]
        public void CreateDefaultClient_WhenCalled_ShouldWrapTheDefaultHandler()
        {
            // Act
            using (var client = HttpClientAdapter.CreateDefaultClient())
            {
                // Assert
                GetHandler(client).Should().BeOfType<SocketsHttpHandler>()
                    .Which.KeepAlivePingPolicy.Should().Be(HttpKeepAlivePingPolicy.Always);
            }
        }

        /// <summary>HttpClient has no public accessor for its handler; HttpMessageInvoker keeps it in a private field.</summary>
        private static HttpMessageHandler GetHandler(HttpClient client)
        {
            var field = typeof(HttpMessageInvoker).GetField("_handler", BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull("HttpMessageInvoker is expected to store its handler in a private field named _handler");
            return (HttpMessageHandler)field.GetValue(client);
        }
    }
}
