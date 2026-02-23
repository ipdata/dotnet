using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using IPData.Exceptions;
using IPData.Tests.DataSources;
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
