using System;
using FluentAssertions;
using IpData.Tests.DataSources;
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
            // Act
            Action act = () => new IpDataClient(apiKey);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .Where(e => e.ParamName == nameof(apiKey))
               .Where(e => e.Message.Contains($"The {nameof(apiKey)} {apiKey} must be not empty or whitespace string"));
        }

        [Theory]
        [AutoMoqData]
        public void IpDataClient_WhenCreatedWithApiKey_ShouldCreateClient(
            string apiKey)
        {
            // Act
            var sut = new IpDataClient(apiKey);

            // Assert
            sut.ApiKey.Should().Be(apiKey);
        }

        [Theory]
        [AutoMoqData]
        public void IpDataClient_WhenCreatedWithInvalidHttpClient_ThrowArgumentNullException(
            string apiKey)
        {
            // Act
            Action act = () => new IpDataClient(apiKey, null);

            // Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }

        [Theory]
        [AutoMoqData]
        public void IpDataClient_WhenCreatedWithApiKeyAndHttpClient_ShouldCreateClient(
            IHttpClient httpClient,
            string apiKey)
        {
            // Act
            Action act = () => new IpDataClient(apiKey, httpClient);

            // Assert
            act.Should().NotThrow();
        }
    }
}
