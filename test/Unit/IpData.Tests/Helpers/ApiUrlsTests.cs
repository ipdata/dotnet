using FluentAssertions;
using IpData.Helpers;
using Xunit;

namespace IpData.Tests.Helpers
{
    public class ApiUrlsTests
    {
        [Theory]
        [AutoMoqData]
        public void Get_WhenCalled_ReturnedUrl(string apiKey)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledWithIp_ReturnedUrl(string apiKey, string ip)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/{ip}?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, ip);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledExpression_ReturnedUrl(string apiKey, string ip)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/{ip}/country_name?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, ip, x => x.CountryName);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Buld_WhenCalled_ReturnedUrl(string apiKey)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/bulk?api-key={apiKey}";

            // Act
            var url = ApiUrls.Bulk(apiKey);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Carrier_WhenCalled_ReturnedUrl(string apiKey, string ip)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/{ip}/carrier?api-key={apiKey}";

            // Act
            var url = ApiUrls.Carrier(apiKey, ip);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }
    }
}
