using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using IPData.Exceptions;
using IPData.Tests.DataSources;
using IPData.Tests.Http;
using Moq;
using Xunit;

namespace IPData.Tests
{
    public class IPDataClientTests
    {
        [Theory]
        [MemberData(nameof(TestDataSource.EmptyOrWhitespaceString), MemberType = typeof(TestDataSource))]
        public void IPDataClient_WhenCreatedWithInvalidApiKey_ThrowArgumentException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IPDataClient(apiKey);

            // Act/Assert
            act.Should()
               .Throw<ArgumentException>()
               .Where(e => e.ParamName == nameof(apiKey))
               .Where(e => e.Message.Contains($"The {nameof(apiKey)} {apiKey} must be not empty or whitespace string"));
        }

        [Theory, AutoMoqData]
        public void IPDataClient_WhenCreatedWithValidApiKey_ShouldCreateClient(
            string apiKey)
        {
            // Act
            var sut = new IPDataClient(apiKey);

            // Assert
            sut.ApiKey.Should().Be(apiKey);
        }

        [Theory, AutoMoqData]
        public void IPDataClient_WhenCreatedWithInvalidIHttpClient_ThrowArgumentNullException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IPDataClient(apiKey, (IHttpClient)null);

            // Act/Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Theory, AutoMoqData]
        public void IPDataClient_WhenCreatedWithValidApiKeyAndHttpClient_ShouldCreateClient(
            IHttpClient httpClient,
            string apiKey)
        {
            // Arrange
            Action act = () => new IPDataClient(apiKey, httpClient);

            // Act/Assert
            act.Should().NotThrow();
        }

        private const string LookupJson =
            "{\"ip\":\"8.8.8.8\",\"is_eu\":false,\"country_name\":\"United States\",\"count\":\"0\"}";

        private const string BulkJson =
            "[{\"ip\":\"1.1.1.1\",\"country_name\":\"Australia\",\"count\":\"0\"}," +
            "{\"ip\":\"2.2.2.2\",\"country_name\":\"France\",\"count\":\"0\"}]";

        private static HttpResponseMessage OkJson(string json) =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

        [Theory, AutoMoqData]
        public void IPDataClient_WhenCreatedWithHttpClientAndBaseUrl_ShouldCreateClient(
            string apiKey)
        {
            // Arrange
            using (var httpClient = new HttpClient())
            {
                // Act
                var sut = new IPDataClient(apiKey, httpClient, new Uri("https://eu-api.ipdata.co"));

                // Assert
                sut.ApiKey.Should().Be(apiKey);
            }
        }

        [Theory, AutoMoqData]
        public void IPDataClient_WhenCreatedWithNullHttpClientAndBaseUrl_ThrowArgumentNullException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IPDataClient(apiKey, (HttpClient)null, new Uri("https://eu-api.ipdata.co"));

            // Act/Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Fact]
        public async Task Lookup_WhenCreatedWithHttpClientAndBaseUrl_ShouldSendThroughThatClientToThatBaseUrl()
        {
            // Arrange
            var handler = new RecordingHttpMessageHandler(LookupJson);
            using (var httpClient = new HttpClient(handler))
            {
                var sut = new IPDataClient("test-api-key", httpClient, new Uri("https://eu-api.ipdata.co"));

                // Act
                var actual = await sut.Lookup("8.8.8.8").ConfigureAwait(false);

                // Assert
                actual.Ip.Should().Be("8.8.8.8");
                handler.Requests.Should().ContainSingle()
                    .Which.RequestUri.AbsoluteUri.Should().Be("https://eu-api.ipdata.co/8.8.8.8?api-key=test-api-key");
            }
        }

        [Fact]
        public async Task Lookup_WhenCalled_ShouldRequestHttp2WithFallbackToLowerVersions()
        {
            // Arrange
            var requests = new List<HttpRequestMessage>();
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(requests.Add)
                .Returns(() => Task.FromResult(OkJson(LookupJson)));

            var sut = new IPDataClient("test-api-key", httpClient.Object);

            // Act
            var actual = await sut.Lookup("8.8.8.8").ConfigureAwait(false);

            // Assert
            actual.Ip.Should().Be("8.8.8.8");
            var request = requests.Should().ContainSingle().Which;
            request.Method.Should().Be(HttpMethod.Get);
            request.Version.Should().Be(HttpVersion.Version20);
            request.VersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrLower);
        }

        [Fact]
        public async Task Lookup_WhenCalledWithIpList_ShouldPostJsonAndRequestHttp2WithFallbackToLowerVersions()
        {
            // Arrange
            var requests = new List<HttpRequestMessage>();
            var bodies = new List<string>();
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(r =>
                {
                    requests.Add(r);
                    // Read the body here: the client disposes the request once the call completes.
                    bodies.Add(r.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                })
                .Returns(() => Task.FromResult(OkJson(BulkJson)));

            var sut = new IPDataClient("test-api-key", httpClient.Object);

            // Act
            var actual = await sut.Lookup(new[] { "1.1.1.1", "2.2.2.2" }).ConfigureAwait(false);

            // Assert
            actual.Select(x => x.Ip).Should().Equal("1.1.1.1", "2.2.2.2");
            var request = requests.Should().ContainSingle().Which;
            request.Method.Should().Be(HttpMethod.Post);
            request.Version.Should().Be(HttpVersion.Version20);
            request.VersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrLower);
            bodies.Should().ContainSingle().Which.Should().Be("[\"1.1.1.1\",\"2.2.2.2\"]");
        }

        [Fact]
        public async Task Lookup_WhenSuccessful_ShouldDisposeRequestAndResponseAfterReadingTheBody()
        {
            // Arrange
            var response = OkJson(BulkJson);
            HttpRequestMessage request = null;
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(r => request = r)
                .Returns(Task.FromResult(response));

            var sut = new IPDataClient("test-api-key", httpClient.Object);

            // Act
            var actual = await sut.Lookup(new[] { "1.1.1.1", "2.2.2.2" }).ConfigureAwait(false);

            // Assert: the body was deserialized before anything was disposed...
            actual.Select(x => x.Ip).Should().Equal("1.1.1.1", "2.2.2.2");

            // ...and both messages are disposed afterwards, so their content can no longer be read.
            Func<Task> readRequest = () => request.Content.ReadAsStringAsync();
            Func<Task> readResponse = () => response.Content.ReadAsStringAsync();
            await readRequest.Should().ThrowAsync<ObjectDisposedException>().ConfigureAwait(false);
            await readResponse.Should().ThrowAsync<ObjectDisposedException>().ConfigureAwait(false);
        }

        [Fact]
        public async Task Lookup_WhenCalledRepeatedlyOnOneInstance_ShouldSendEveryRequestThroughTheSameHttpClient()
        {
            // Arrange
            var handler = new RecordingHttpMessageHandler(LookupJson);
            using (var httpClient = new HttpClient(handler))
            {
                var sut = new IPDataClient("test-api-key", httpClient);

                // Act
                await sut.Lookup().ConfigureAwait(false);
                await sut.Lookup("8.8.8.8").ConfigureAwait(false);
                await sut.Threat("8.8.8.8").ConfigureAwait(false);

                // Assert
                handler.Requests.Should().HaveCount(3);
                handler.Requests.Should().OnlyContain(r => r.Version == HttpVersion.Version20);
                handler.Requests.Should().OnlyContain(r => r.VersionPolicy == HttpVersionPolicy.RequestVersionOrLower);
            }
        }

        public static IEnumerable<object[]> StatusCodeExceptionData()
        {
            yield return new object[] { HttpStatusCode.BadRequest, typeof(BadRequestException) };
            yield return new object[] { HttpStatusCode.Forbidden, typeof(ForbiddenException) };
            yield return new object[] { HttpStatusCode.Unauthorized, typeof(UnauthorizedException) };
            yield return new object[] { (HttpStatusCode)0, typeof(ApiException) };
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Lookup_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup().ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Lookup_WhenCalledWithIpAndErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("69.78.70.144").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Lookup_WhenCalledWithIpListAndErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            var ipList = new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" };
            Func<Task> act = async () => { await sut.Lookup(ipList).ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Lookup_WhenCalledWithSelectorAndErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.CountryName).ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Lookup_WhenCalledWithSelectorsAndErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.Asn, x => x.City).ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Company_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Company("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Carrier_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Carrier("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Asn_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Asn("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task TimeZone_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.TimeZone("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Currency_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Currency("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }

        [Theory]
        [MemberData(nameof(StatusCodeExceptionData))]
        public async Task Threat_WhenErrorStatusCode_ShouldThrowExpectedException(
            HttpStatusCode statusCode, Type expectedExceptionType)
        {
            // Arrange
            var httpClient = new Mock<IHttpClient>();
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            var sut = new IPDataClient("test-api-key", httpClient.Object);
            Func<Task> act = async () => { await sut.Threat("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            (await act.Should().ThrowAsync<ApiException>().ConfigureAwait(false))
                .And.Should().BeOfType(expectedExceptionType);
        }
    }
}
