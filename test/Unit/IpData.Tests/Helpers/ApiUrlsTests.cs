using FluentAssertions;
using IpData.Helpers;
using System.Globalization;
using Xunit;

namespace IpData.Tests.Helpers
{
    public class ApiUrlsTests
    {
        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledWithInvariantCulture_ReturnedUrl(string apiKey)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, CultureInfo.InvariantCulture);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledWithCulture_ReturnedUrl(string apiKey, CultureInfo culture)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/{culture}?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, culture);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledWithIpAndInvariantCulture_ReturnedUrl(string apiKey, string ip)
        {
            // Arrange
            var expectedUrl = $"https://api.ipdata.co/{ip}?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, ip, CultureInfo.InvariantCulture);

            // Assert
            url.AbsoluteUri.Should().Be(expectedUrl);
        }

        [Theory]
        [AutoMoqData]
        public void Get_WhenCalledWithIpAndCulture_ReturnedUrl(string apiKey, string ip)
        {
            // Arrange
            var culture = CultureInfo.GetCultureInfo("zh-CN");
            var expectedUrl = $"https://api.ipdata.co/{ip}/{culture}?api-key={apiKey}";

            // Act
            var url = ApiUrls.Get(apiKey, ip, culture);

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
