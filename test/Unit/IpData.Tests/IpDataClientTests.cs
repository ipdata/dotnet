using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IpData.Exceptions;
using Moq;
using Xunit;

namespace IpData.Tests
{
    public class IpDataClientTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void IpDataClient_WhenCreatedWithInvalidApiKey_ThrowArgumentException(string invalidApiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(invalidApiKey);

            // Act/Assert
            act.Should().Throw<ArgumentException>()
                .Which.Message.Should().NotBeNullOrEmpty();
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithValidApiKey_ShouldCreateClient(string validApiKey)
        {
            // Act
            var sut = new IpDataClient(validApiKey);

            // Assert
            sut.ApiKey.Should().Be(validApiKey);
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithInvalidIHttpClient_ThrowArgumentNullException(string validApiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(validApiKey, null as IHttpClient);

            // Act/Assert
            act.Should().Throw<ArgumentNullException>()
                .Which.Message.Should().NotBeNullOrEmpty();
        }

        [Theory, AutoMoqData]
        public void IpDataClient_WhenCreatedWithValidApiKeyAndHttpClient_ShouldCreateClient(
            IHttpClient httpClient,
            string validApiKey)
        {
            // Arrange
            Action act = () => new IpDataClient(validApiKey, httpClient);

            // Act/Assert
            act.Should().NotThrow();
        }
        
        [Fact]
        public Task Lookup_WhenStatusCode400_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Lookup();

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Lookup_WhenStatusCode403_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () =>  sut.Lookup();

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Lookup_WhenStatusCode401_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Lookup();

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Lookup_WhenUnknownStatusCode_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Lookup();

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIpList_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            var ipList = new[] { IPAddress.Any.ToString(), IPAddress.Broadcast.ToString() };
            Func<Task> act = () => sut.Lookup(ipList);

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIpList_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            var ipList = new[] { IPAddress.Any.ToString(), IPAddress.Broadcast.ToString() };
            Func<Task> act = () => sut.Lookup(ipList);

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIpList_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            var ipList = new[] { IPAddress.Any.ToString(), IPAddress.Broadcast.ToString() };
            Func<Task> act = () => sut.Lookup(ipList);

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithIpList_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            var ipList = new[] { IPAddress.Any.ToString(), IPAddress.Broadcast.ToString() };
            Func<Task> act = () => sut.Lookup(ipList);

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelector_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.CountryName);

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelector_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.CountryName);

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelector_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.CountryName);

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelector_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.CountryName);

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelectors_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.Asn, x => x.City);

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelectors_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = async () => { await sut.Lookup(IPAddress.Any.ToString(), x => x.Asn, x => x.City); };

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelectors_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.Asn, x => x.City);

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Lookup_WhenCalledWithSelectors_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Lookup(IPAddress.Any.ToString(), x => x.Asn, x => x.City);

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Carrier_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Carrier(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Carrier_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Carrier(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Carrier_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Carrier(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Carrier_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Carrier(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Asn_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Asn(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Asn_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Asn(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Asn_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Asn(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Asn_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Asn(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task TimeZone_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.TimeZone(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task TimeZone_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.TimeZone(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task TimeZone_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.TimeZone(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task TimeZone_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.TimeZone(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Currency_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Currency(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Currency_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Currency(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Currency_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Currency(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Currency_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Currency(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public Task Threat_WhenCalledWithIp_ShouldThrowBadRequestException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.BadRequest);
            Func<Task> act = () => sut.Threat(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public Task Threat_WhenCalledWithIp_ShouldThrowForbiddenException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Forbidden);
            Func<Task> act = () => sut.Threat(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public Task Threat_WhenCalledWithIp_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.Unauthorized);
            Func<Task> act = () => sut.Threat(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<UnauthorizedException>();
        }

        [Fact]
        public Task Threat_WhenCalledWithIp_ShouldThrowApiException()
        {
            // Arrange
            var sut = SetupSutWithStatusCode(HttpStatusCode.InternalServerError);
            Func<Task> act = () => sut.Threat(IPAddress.Any.ToString());

            // Act/Assert
            return act.Should().ThrowAsync<ApiException>();
        }

        private static IpDataClient SetupSutWithStatusCode(HttpStatusCode statusCode)
        {
            var httpClient = new Mock<IHttpClient>();

            httpClient
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(statusCode));

            return new IpDataClient("apiKey", httpClient.Object);
        }
    }
}