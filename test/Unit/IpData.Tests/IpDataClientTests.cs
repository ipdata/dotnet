using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using IpData.Exceptions;
using IpData.Tests.DataSources;
using Moq;
using Xunit;

namespace IpData.Tests
{
    public class IpDataClientTests
    {
        [Theory]
        [MemberData(nameof(TestDataSource.EmptyOrWhitespaceString), MemberType = typeof(TestDataSource))]
        public void IpDataClient_WhenCreatedWithInvalidApiKey_ThrowArgumentException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(apiKey);

            // Act/Assert
            act.Should()
               .Throw<ArgumentException>()
               .Where(e => e.ParamName == nameof(apiKey))
               .Where(e => e.Message.Contains($"The {nameof(apiKey)} {apiKey} must be not empty or whitespace string"));
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithApiKey_ShouldCreateClient(
            string apiKey)
        {
            // Act
            var sut = new IpDataClient(apiKey);

            // Assert
            sut.ApiKey.Should().Be(apiKey);
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithInvalidIHttpClient_ThrowArgumentNullException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(apiKey, (IHttpClient)null);

            // Act/Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithInvalidHttpClient_ThrowArgumentNullException(
            string apiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(apiKey, (HttpClient)null);

            // Act/Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithApiKeyAndHttpClient_ShouldCreateClient(
            IHttpClient httpClient,
            string apiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(apiKey, httpClient);

            // Act/Assert
            act.Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalled_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup().ConfigureAwait(false); };
            
            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalled_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup().ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalled_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup().ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalled_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup().ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("69.78.70.144").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("69.78.70.144").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("69.78.70.144").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("69.78.70.144").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIpList_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            var ipList = new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" };
            Func<Task> act = async () => { await sut.Lookup(ipList).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIpList_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            var ipList = new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" };
            Func<Task> act = async () => { await sut.Lookup(ipList).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIpList_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            var ipList = new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" };
            Func<Task> act = async () => { await sut.Lookup(ipList).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithIpList_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            var ipList = new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" };
            Func<Task> act = async () => { await sut.Lookup(ipList).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithSelector_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.CountryName).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithSelector_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.CountryName).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithSelector_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.CountryName).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Lookup_WhenCalledWithSelector_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Lookup("1.1.1.1", x => x.CountryName).ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Carrier_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Carrier("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Carrier_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Carrier("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Carrier_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Carrier("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Carrier_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Carrier("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Asn_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Asn("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Asn_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Asn("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Asn_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Asn("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Asn_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Asn("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task TimeZone_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.TimeZone("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task TimeZone_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.TimeZone("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task TimeZone_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.TimeZone("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task TimeZone_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.TimeZone("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Currency_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Currency("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Currency_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Currency("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Currency_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Currency("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Currency_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Currency("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Threat_WhenCalledWithIp_ShouldThrowBadRequestException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Threat("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<BadRequestException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Threat_WhenCalledWithIp_ShouldThrowForbiddenException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Threat("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ForbiddenException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Threat_WhenCalledWithIp_ShouldThrowUnauthorizedException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Threat("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<UnauthorizedException>()
                .ConfigureAwait(false);
        }

        [Theory, AutoMoqData]
        public async Task Threat_WhenCalledWithIp_ShouldThrowApiException(
            [Frozen] Mock<IHttpClient> httpClient,
            string apiKey)
        {
            // Arrange
            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(0));

            var sut = new IpDataClient(apiKey, httpClient.Object);
            Func<Task> act = async () => { await sut.Threat("1.1.1.1").ConfigureAwait(false); };

            // Act/Assert
            await act.Should()
                .ThrowAsync<ApiException>()
                .ConfigureAwait(false);
        }
    }
}
